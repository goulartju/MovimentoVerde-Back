namespace Mov.Domain.Dtos.Doacao;

public class DoacaoFilterDto
{
    public Guid? CalendarioId { get; set; }
    public DateTime? Data { get; set; }
    public Guid? EscolaId { get; set; }
    public Guid? TurmaId { get; set; }
}
