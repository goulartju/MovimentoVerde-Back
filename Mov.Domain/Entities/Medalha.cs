namespace Mov.Domain.Entities;

public class Medalha
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Qtd { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}