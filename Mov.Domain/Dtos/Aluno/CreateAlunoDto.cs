namespace Mov.Domain.Dtos.Aluno;

public class CreateAlunoDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public bool Ativo { get; set; } = true;
}
