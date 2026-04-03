namespace Mov.Domain.Entities;

public class Doacao
{
    public int Id { get; set; }
    public int MatriculaId { get; set; }
    public Guid EscolaId { get; set; }
    public Guid CalendarioId { get; set; }
    public int QldLacre { get; set; }
    public int QldTampinha { get; set; }
    public DateTime Data { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }

    public Matricula? Matricula { get; set; }
    public Escola? Escola { get; set; }
    public Calendario? Calendario { get; set; }
}
