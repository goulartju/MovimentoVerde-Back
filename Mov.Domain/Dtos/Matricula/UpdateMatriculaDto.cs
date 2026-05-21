namespace Mov.Domain.Dtos.Matricula;

public class UpdateMatriculaDto
{
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public Guid CalendarioId { get; set; }
    public Guid EscolaId { get; set; }
    public string Status { get; set; } = string.Empty;
}
