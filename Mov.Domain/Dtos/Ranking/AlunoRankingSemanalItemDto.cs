namespace Mov.Domain.Dtos.Ranking;

/// <summary>
/// DTO para representar um aluno no ranking semanal
/// </summary>
public class AlunoRankingSemanalItemDto : AlunoRankingItemDto
{
    public string? DataReferencia { get; set; }
    public string? Periodo { get; set; }
}
