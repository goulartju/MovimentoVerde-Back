namespace Mov.Domain.Entities;

public class RepresentanteTurma
{
    public Guid Id { get; set; }
    public Guid TurmaId { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime AtribuidoEm { get; set; }

    // Propriedades de navegação
    public Turma? Turma { get; set; }
    public Usuario? Usuario { get; set; }
}
