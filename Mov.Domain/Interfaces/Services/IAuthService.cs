using Mov.Domain.Dtos.Auth;
using Mov.Domain.Dtos.Usuario;

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

