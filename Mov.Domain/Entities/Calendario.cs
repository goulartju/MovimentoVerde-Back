namespace Mov.Domain.Entities;

public class Calendario
{
    public Guid Id { get; set; }
    public string Ano { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}
