namespace Mov.Domain.Dtos.Aluno;

public class CreateAlunoDto
{
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public Guid? TurmaId { get; set; }
}
