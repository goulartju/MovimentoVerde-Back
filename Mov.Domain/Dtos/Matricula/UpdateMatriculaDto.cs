namespace Mov.Domain.Dtos.Matricula;

public class UpdateMatriculaDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public Guid CalendarioId { get; set; }
    public string Status { get; set; } = string.Empty;
}
