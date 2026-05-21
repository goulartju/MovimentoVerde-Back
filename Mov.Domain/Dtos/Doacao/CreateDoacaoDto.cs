namespace Mov.Domain.Dtos.Doacao;

public class CreateDoacaoItemDto
{
    public Guid MatriculaId { get; set; }
    public int QtdLacre { get; set; }
    public int QtdTampinha { get; set; }

  
}

public class CreateDoacaoLoteDto
{

    public Guid EscolaId { get; set; }
    public Guid CalendarioId { get; set; }
    public DateTime Data { get; set; }

    public List<CreateDoacaoItemDto> Doacoes { get; set; }

}