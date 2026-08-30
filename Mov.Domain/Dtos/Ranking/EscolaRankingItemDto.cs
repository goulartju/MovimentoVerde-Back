namespace Mov.Domain.Dtos.Ranking;

/// <summary>
/// DTO para representar uma escola no ranking
/// </summary>
public class EscolaRankingItemDto
{
    public int Posicao { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int QuantidadeTampinhas { get; set; }
    public int QuantidadeLacres { get; set; }
    public int Total { get; set; }
    public string? EscolaId { get; set; }
}
