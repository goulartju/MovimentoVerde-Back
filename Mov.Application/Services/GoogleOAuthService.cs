using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Mov.Domain.Dtos.Auth;
using Mov.Domain.Entities;
using Mov.Domain.Enums;
using Mov.Domain.Exceptions;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Settings;

namespace Mov.Application.Services;

public class GoogleOAuthService
{
    private readonly GoogleOAuthSettings _googleSettings;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly HttpClient _httpClient;
    private Dictionary<string, JsonWebKey>? _cachedKeys;
    private DateTime _keysCachedUntil = DateTime.MinValue;

    public GoogleOAuthService(
        GoogleOAuthSettings googleSettings,
        IUsuarioRepository usuarioRepository,
        JwtSettings jwtSettings,
        HttpClient httpClient)
    {
        _googleSettings = googleSettings;
        _usuarioRepository = usuarioRepository;
        _jwtSettings = jwtSettings;
        _httpClient = httpClient;
    }

    /// <summary>
    /// Valida o ID Token do Google e cria/atualiza o usuário no banco
    /// </summary>
    public async Task<TokenResponseDto> ValidateGoogleTokenAsync(string idToken)
    {
        try
        {
            // 1. Validar e decodificar o token
            var principal = await ValidateGoogleIdTokenAsync(idToken);

            // 2. Extrair claims do token
            var email = principal.FindFirst(ClaimTypes.Email)?.Value
             ?? principal.FindFirst("email")?.Value;

            var name = principal.FindFirst(ClaimTypes.Name)?.Value
                       ?? principal.FindFirst("name")?.Value;

            var sub = principal.FindFirst("sub")?.Value
                      ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sub))
            {
                throw new InvalidOperationException("Token inválido: email ou sub claim ausente");
            }

            // 3. Buscar usuário pré-cadastrado (sem auto-criação por padrão)
            var usuario = await _usuarioRepository.GetByEmailAsync(email);

            if (usuario == null)
            {
                if (!_googleSettings.AllowNewUsersViaGoogle)
                {
                    throw new UnauthorizedAccessException("Usuário não autorizado. Solicite acesso ao administrador.");
                }

                // Criar novo usuário com dados do Google
                usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = name ?? email.Split('@')[0],
                    Email = email,
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString()), // Senha aleatória
                    GoogleId = sub, // Armazenar ID do Google
                    Ativo = true,
                    Permissao = PermissaoEnum.Visualizador,
                    CriadoEm = DateTime.UtcNow
                };

                await _usuarioRepository.CreateAsync(usuario);
            }
            else
            {
                // Atualizar GoogleId se necessário
                if (string.IsNullOrEmpty(usuario.GoogleId))
                {
                    usuario.GoogleId = sub;
                    await _usuarioRepository.UpdateAsync(usuario);
                }

                // Verificar se usuário está ativo
                if (!usuario.Ativo)
                {
                    throw new UnauthorizedAccessException("Usuário inativo");
                }
            }

            // 4. Gerar JWT interno
            return new TokenResponseDto
            {
                AccessToken = GenerateAccessToken(usuario),
                RefreshToken = GenerateRefreshToken(),
                TokenType = "Bearer",
                ExpiresIn = _jwtSettings.AccessTokenExpirationMinutes * 60,
                Usuario = new Mov.Domain.Dtos.Usuario.UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Cargo = usuario.Cargo,
                    Permissao = (int)usuario.Permissao,
                    Ativo = usuario.Ativo
                }
            };
        }
        catch (SecurityTokenException ex)
        {
            throw new GoogleTokenValidationException($"Token Google inválido: {ex.Message}", ex);
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (GoogleTokenValidationException)
        {
            throw;
        }
        catch (GoogleAuthenticationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new GoogleAuthenticationException($"Erro ao processar login Google: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Valida o ID Token recebido do Google
    /// </summary>
    private async Task<ClaimsPrincipal> ValidateGoogleIdTokenAsync(string idToken)
    {
        var handler = new JwtSecurityTokenHandler();

        // Obter as chaves públicas do Google (com cache)
        var keys = await GetGooglePublicKeysAsync();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = keys.Values,
            ValidateIssuer = true,
            ValidIssuer = _googleSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _googleSettings.ClientId,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };

        try
        {
            var principal = handler.ValidateToken(idToken, tokenValidationParameters, out SecurityToken validatedToken);
            return principal;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new GoogleTokenValidationException("Token Google expirado");
        }
        catch (SecurityTokenValidationException ex)
        {
            throw new GoogleTokenValidationException($"Validação de token falhou: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Obtém as chaves públicas do Google (com cache)
    /// </summary>
    private async Task<Dictionary<string, JsonWebKey>> GetGooglePublicKeysAsync()
    {
        // Usar cache se ainda válido
        if (_cachedKeys != null && DateTime.UtcNow < _keysCachedUntil)
        {
            return _cachedKeys;
        }

        try
        {
            var response = await _httpClient.GetAsync(_googleSettings.CertificatesUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[GOOGLE CERTS] Response: {content.Substring(0, Math.Min(200, content.Length))}...");

            var jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

            _cachedKeys = new Dictionary<string, JsonWebKey>();

            // Verificar se temos a propriedade "keys" (formato v3)
            if (root.TryGetProperty("keys", out var keysElement))
            {
                // Formato v3: { "keys": [...] }
                foreach (var keyElement in keysElement.EnumerateArray())
                {
                    try
                    {
                        var keyJson = keyElement.GetRawText();
                        var jwk = new JsonWebKey(keyJson);

                        if (keyElement.TryGetProperty("kid", out var kidElement))
                        {
                            var kid = kidElement.GetString();
                            if (!string.IsNullOrEmpty(kid))
                            {
                                _cachedKeys[kid] = jwk;
                                Console.WriteLine($"[GOOGLE CERTS] Chave adicionada: {kid}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[GOOGLE CERTS] Erro ao deserializar chave: {ex.Message}");
                    }
                }
            }
            else
            {
                // Formato v1: { "kid-1": {...}, "kid-2": {...} }
                foreach (var property in root.EnumerateObject())
                {
                    try
                    {
                        var keyJson = property.Value.GetRawText();
                        var jwk = new JsonWebKey(keyJson);
                        _cachedKeys[property.Name] = jwk;
                        Console.WriteLine($"[GOOGLE CERTS] Chave adicionada (v1): {property.Name}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[GOOGLE CERTS] Erro ao deserializar chave {property.Name}: {ex.Message}");
                    }
                }
            }

            if (_cachedKeys.Count == 0)
            {
                throw new InvalidOperationException($"Nenhuma chave pública válida foi obtida do Google. Response: {content.Substring(0, Math.Min(500, content.Length))}");
            }

            _keysCachedUntil = DateTime.UtcNow.AddMinutes(_googleSettings.CertificateCacheDurationMinutes);
            Console.WriteLine($"[GOOGLE CERTS] {_cachedKeys.Count} chaves carregadas com sucesso");

            return _cachedKeys;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"[GOOGLE CERTS] Erro HTTP: {ex.Message}");
            throw new GoogleAuthenticationException($"Erro ao obter chaves públicas do Google: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GOOGLE CERTS] Erro genérico: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Gera um Access Token JWT interno
    /// </summary>
    private string GenerateAccessToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(_jwtSettings.Secret);

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

    /// <summary>
    /// Gera um Refresh Token
    /// </summary>
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
