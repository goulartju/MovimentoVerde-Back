using Mov.Domain.Dtos.Usuario;

namespace Mov.Domain.Dtos.Auth;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";

    // Dados do usuário para o front (permissão vai dentro de Usuario.Permissao)
    public UsuarioDto Usuario { get; set; } = new();
}
