namespace Mov.Domain.Dtos.Ranking;

/// <summary>
/// DTO para representar uma turma no ranking
/// </summary>
public class TurmaRankingItemDto
{
    public int Posicao { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int QuantidadeTampinhas { get; set; }
    public int QuantidadeLacres { get; set; }
    public int Total { get; set; }
    public string? TurmaId { get; set; }
    public string? EscolaId { get; set; }
    public string? EscolaNome { get; set; }
}
