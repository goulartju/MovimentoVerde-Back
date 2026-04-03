namespace Mov.Domain.Dtos.Calendario;

public class UpdateCalendarioDto
{
    public Guid Id { get; set; }
    public Guid EscolaId { get; set; }
    public int Ano { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public bool Ativo { get; set; }
}
