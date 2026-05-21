namespace Mov.Domain.Dtos.Matricula;

public class MatriculaDto
{
    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }
    public string NomeAluno { get; set; } = string.Empty;
    public Guid TurmaId { get; set; }
    public string NomeTurma { get; set; } = string.Empty;
    public int AnoEscolar { get; set; }
    public Guid CalendarioId { get; set; }
    public string NomeCalendario { get; set; } = string.Empty;
    public Guid EscolaId { get; set; }
    public string NomeEscola { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
