using Mov.Domain.Dtos.Auth;
using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
    Task<TokenResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
    Task<bool> ValidateTokenAsync(string token);
    Task<UsuarioDto> CreateUserAsync(CreateUserDto createUserDto, Guid adminId);
    Task<bool> ChangePasswordAsync(Guid usuarioId, ChangePasswordDto changePasswordDto);
}

public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public int Permissao { get; set; }
    public bool Ativo { get; set; }
}
