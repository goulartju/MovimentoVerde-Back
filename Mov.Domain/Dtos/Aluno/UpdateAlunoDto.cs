namespace Mov.Domain.Dtos.Aluno;

public class UpdateAlunoDto
{
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public Guid? TurmaId { get; set; }
}
