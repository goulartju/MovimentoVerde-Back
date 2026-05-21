using Mov.Domain.Enums;

namespace Mov.Domain.Dtos.Usuario;

public class CreateUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public PermissaoEnum Permissao { get; set; }
    public bool Ativo { get; set; }
}
