namespace Mov.Domain.Dtos.Matricula;

public class CreateMatriculaDto
{
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public Guid CalendarioId { get; set; }
    public Guid EscolaId { get; set; }
    public string Status { get; set; } = string.Empty;
}
