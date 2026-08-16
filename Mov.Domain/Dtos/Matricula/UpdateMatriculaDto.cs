namespace Mov.Domain.Dtos.Matricula;

public class UpdateMatriculaDto
{
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public Guid CalendarioId { get; set; }
    public bool Ativo { get; set; }
}
