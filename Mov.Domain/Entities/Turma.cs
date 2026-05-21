namespace Mov.Domain.Entities;

public class Turma
{
    public Guid Id { get; set; }
    public Guid EscolaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Mov.Domain.Enums.AnoSerieEnum AnoEscolar { get; set; }
    public Mov.Domain.Enums.TurnoEnum Turno { get; set; }
    public Guid CalendarioId { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }

    // Propriedades de navegação
    public Escola? Escola { get; set; }
    public Calendario? Calendario { get; set; }
    public RepresentanteTurma? Representante { get; set; } 
    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
