namespace Mov.Domain.Dtos.Ranking;

/// <summary>
/// DTO para representar um aluno no ranking de turma
/// </summary>
public class TurmaAlunoRankingItemDto
{
    public int Posicao { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int QuantidadeTampinhas { get; set; }
    public int QuantidadeLacres { get; set; }
    public int Total { get; set; }
    public string? Medalha { get; set; }
}
