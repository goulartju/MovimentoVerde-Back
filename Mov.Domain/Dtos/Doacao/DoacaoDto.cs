namespace Mov.Domain.Dtos.Doacao;

public class DoacaoDto
{
    public int Id { get; set; }
    public int MatriculaId { get; set; }
    public Guid EscolaId { get; set; }
    public Guid CalendarioId { get; set; }
    public int QldLacre { get; set; }
    public int QldTampinha { get; set; }
    public DateTime Data { get; set; }
    public string NomeAluno { get; set; } = string.Empty;
    public string NomeTurma { get; set; } = string.Empty;
    public string NomeEscola { get; set; } = string.Empty;
    public string NomeCalendario { get; set; } = string.Empty;
    public int AnoEscolar { get; set; }
}
