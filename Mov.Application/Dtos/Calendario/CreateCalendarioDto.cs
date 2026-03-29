namespace Mov.Application.Dtos.Calendario;

public class CreateCalendarioDto
{
    public string Ano { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}
