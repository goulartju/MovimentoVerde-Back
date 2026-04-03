namespace Mov.Domain.Dtos.Auth;

using Mov.Domain.Enums;

public class CreateUserDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cargo { get; set; } = string.Empty;
    public PermissaoEnum Permissao { get; set; }
}
