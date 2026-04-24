namespace Mov.Domain.Dtos.Turma;

public class TurmaDto
{
    public Guid Id { get; set; }
    public Guid EscolaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int AnoEscolar { get; set; }
    public string Turno { get; set; } = string.Empty;
    public Guid CalendarioId { get; set; }
    public bool Ativo { get; set; }
    public Guid? RepresentanteId { get; set; }
}
