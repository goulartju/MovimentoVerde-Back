namespace Mov.Domain.Entities;

public class Turma
{
    public Guid Id { get; set; }
    public Guid EscolaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int AnoEscolar { get; set; }
    public string Turno { get; set; } = string.Empty;
    public Guid CalendarioId { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }

    // Propriedades de navegação
    public Escola? Escola { get; set; }
    public Calendario? Calendario { get; set; }
    public ICollection<RepresentanteTurma> Representantes { get; set; } = new List<RepresentanteTurma>();
}
