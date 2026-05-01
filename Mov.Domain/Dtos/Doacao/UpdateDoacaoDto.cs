namespace Mov.Domain.Dtos.Doacao;

public class UpdateDoacaoItemDto
{
    public Guid Id { get; set; }
    public Guid MatriculaId { get; set; }
    public int QtdLacre { get; set; }
    public int QtdTampinha { get; set; }

}

public class UpdateDoacaoLoteDto
{

    public Guid EscolaId { get; set; }
    public Guid CalendarioId { get; set; }
    public DateTime Data { get; set; }

    public List<UpdateDoacaoItemDto> Doacoes { get; set; }

}