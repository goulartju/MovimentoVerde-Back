namespace Mov.Domain.Dtos.Turma;

public class CreateTurmaDto
{
    public Guid EscolaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int AnoEscolar { get; set; }
    public string Turno { get; set; } = string.Empty;
    public Guid CalendarioId { get; set; }
    public bool Ativo { get; set; } = true;
    public ICollection<RepresentanteDto>? Representantes { get; set; }
}
