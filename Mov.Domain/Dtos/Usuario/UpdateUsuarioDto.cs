using Mov.Domain.Enums;

namespace Mov.Domain.Dtos.Usuario;

public class UpdateUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public PermissaoEnum Permissao { get; set; }
    public bool Ativo { get; set; }
}
