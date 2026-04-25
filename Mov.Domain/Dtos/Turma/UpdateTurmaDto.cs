namespace Mov.Domain.Dtos.Turma;

public class UpdateTurmaDto
{
    public Guid EscolaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string AnoEscolar { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;
    public Guid CalendarioId { get; set; }
    public bool Ativo { get; set; }
    public Guid? RepresentanteId { get; set; }
}
