namespace Mov.Domain.Dtos.Medalha;

public class MedalhaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Qtd { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}