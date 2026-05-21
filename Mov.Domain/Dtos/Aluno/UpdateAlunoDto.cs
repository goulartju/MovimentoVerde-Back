namespace Mov.Domain.Dtos.Aluno;

public class UpdateAlunoDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public bool Ativo { get; set; }
}
