using Mov.Domain.Enums;

namespace Mov.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public string? GoogleId { get; set; } // ID do Google para OAuth
    public PermissaoEnum Permissao { get; set; }
    public bool Ativo { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
}
