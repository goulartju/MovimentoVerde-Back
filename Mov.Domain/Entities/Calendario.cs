namespace Mov.Domain.Entities;

public class Calendario
{
    public Guid Id { get; set; }
    public Guid EscolaId { get; set; }
    public int Ano { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public bool Ativo { get; set; }

    // Propriedade de navegação
    public Escola? Escola { get; set; }
}
