using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Mov.Domain.Dtos.Auth;
using Mov.Domain.Dtos.Usuario;
using Mov.Domain.Interfaces.Services;
using Mov.Domain.Settings;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Entities;
using Mov.Domain.Enums;
using FluentValidation;

namespace Mov.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly IValidator<LoginDto> _loginValidator;
    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<ChangePasswordDto> _changePasswordValidator;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        JwtSettings jwtSettings,
        IValidator<LoginDto> loginValidator,
        IValidator<RegisterDto> registerValidator,
        IValidator<CreateUserDto> createUserValidator,
        IValidator<ChangePasswordDto> changePasswordValidator)
    {
        _usuarioRepository = usuarioRepository;
        _jwtSettings = jwtSettings;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _createUserValidator = createUserValidator;
        _changePasswordValidator = changePasswordValidator;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
    {
        await _loginValidator.ValidateAndThrowAsync(loginDto);

        var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.SenhaHash))
        {
            throw new UnauthorizedAccessException("Email ou senha inválidos");
        }

        if (!usuario.Ativo)
        {
            throw new UnauthorizedAccessException("Usuário inativo");
        }

        var accessToken = GenerateAccessToken(usuario);
        var refreshToken = GenerateRefreshToken();

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = _jwtSettings.AccessTokenExpirationMinutes * 60,
            TokenType = "Bearer"
        };
    }

    public async Task<TokenResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        await _registerValidator.ValidateAndThrowAsync(registerDto);

        var usuarioExistente = await _usuarioRepository.GetByEmailAsync(registerDto.Email);
        if (usuarioExistente != null)
        {
            throw new InvalidOperationException("Email já está registrado");
        }

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = registerDto.Nome,
            Email = registerDto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Senha),
            DataNascimento = registerDto.DataNascimento,
            Cargo = registerDto.Cargo,
            Permissao = PermissaoEnum.Visualizador,
            Ativo = true,
            CriadoEm = DateTime.UtcNow
        };

        await _usuarioRepository.CreateAsync(usuario);

        var accessToken = GenerateAccessToken(usuario);
        var refreshToken = GenerateRefreshToken();

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = _jwtSettings.AccessTokenExpirationMinutes * 60,
            TokenType = "Bearer"
        };
    }

    public async Task<UsuarioDto> CreateUserAsync(CreateUserDto createUserDto, Guid adminId)
    {
        await _createUserValidator.ValidateAndThrowAsync(createUserDto);

        var admin = await _usuarioRepository.GetByIdAsync(adminId);
        if (admin == null || admin.Permissao != PermissaoEnum.Administrador)
        {
            throw new UnauthorizedAccessException("Apenas administradores podem criar usuários");
        }

        var usuarioExistente = await _usuarioRepository.GetByEmailAsync(createUserDto.Email);
        if (usuarioExistente != null)
        {
            throw new InvalidOperationException("Email já está registrado");
        }

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = createUserDto.Nome,
            Email = createUserDto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Senha),
            DataNascimento = createUserDto.DataNascimento,
            Cargo = createUserDto.Cargo,
            Permissao = createUserDto.Permissao,
            Ativo = true,
            CriadoEm = DateTime.UtcNow
        };

        await _usuarioRepository.CreateAsync(usuario);

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Cargo = usuario.Cargo,
            Permissao = usuario.Permissao.ToString(),
            Ativo = usuario.Ativo
        };
    }

    public async Task<bool> ChangePasswordAsync(Guid usuarioId, ChangePasswordDto changePasswordDto)
    {
        await _changePasswordValidator.ValidateAndThrowAsync(changePasswordDto);

        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
        if (usuario == null)
        {
            throw new InvalidOperationException("Usuário não encontrado");
        }

        if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.SenhaAtual, usuario.SenhaHash))
        {
            throw new UnauthorizedAccessException("Senha atual inválida");
        }

        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NovaSenha);
        usuario.AtualizadoEm = DateTime.UtcNow;

        await _usuarioRepository.UpdateAsync(usuario);
        return true;
    }

    public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException("Refresh token inválido");
        }

        throw new NotImplementedException("Refresh token validation needs database storage");
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private string GenerateAccessToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Permissao.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
