namespace Mov.Domain.Entities;

public class Matricula
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public Guid CalendarioId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }

    public Aluno? Aluno { get; set; }
    public Turma? Turma { get; set; }
    public Calendario? Calendario { get; set; }
    public ICollection<Doacao> Doacoes { get; set; } = new List<Doacao>();
}
