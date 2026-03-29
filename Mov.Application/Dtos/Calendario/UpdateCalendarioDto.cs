namespace Mov.Application.Dtos.Calendario;

public class UpdateCalendarioDto
{
    public Guid Id { get; set; }
    public string Ano { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}
