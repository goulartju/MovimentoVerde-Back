namespace Mov.Domain.Dtos.Ranking;

/// <summary>
/// DTO para representar um aluno no ranking
/// </summary>
public class AlunoRankingItemDto
{
    public int Posicao { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int QuantidadeTampinhas { get; set; }
    public int QuantidadeLacres { get; set; }
    public int Total { get; set; }
    public string? Medalha { get; set; }
    public string? Turma { get; set; }
    public string? Escola { get; set; }
}
