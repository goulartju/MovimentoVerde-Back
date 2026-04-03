namespace Mov.Domain.Entities;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }

    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
