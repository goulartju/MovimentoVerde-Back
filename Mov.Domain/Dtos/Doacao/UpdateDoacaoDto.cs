namespace Mov.Domain.Dtos.Doacao;

public class UpdateDoacaoDto
{
    public int Id { get; set; }
    public Guid MatriculaId { get; set; }
    public Guid EscolaId { get; set; }
    public Guid CalendarioId { get; set; }
    public int QldLacre { get; set; }
    public int QldTampinha { get; set; }
    public DateTime Data { get; set; }
}
