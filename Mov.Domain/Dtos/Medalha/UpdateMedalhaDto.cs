namespace Mov.Domain.Dtos.Medalha;

public class UpdateMedalhaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Qtd { get; set; }
}