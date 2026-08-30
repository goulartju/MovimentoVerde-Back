using Mov.Domain.Dtos.Ranking;

namespace Mov.Domain.Interfaces.Services;

/// <summary>
/// Interface para o serviço de rankings
/// </summary>
public interface IRankingService
{
    /// <summary>
    /// Obtém o ranking de alunos de uma determinada escola
    /// </summary>
    Task<IEnumerable<AlunoRankingItemDto>> GetRankingAlunosAsync(Guid? escolaId = null);

    /// <summary>
    /// Obtém o ranking de alunos da última semana de uma determinada escola
    /// </summary>
    Task<IEnumerable<AlunoRankingSemanalItemDto>> GetRankingSemanalAlunosAsync(Guid? escolaId = null);

    /// <summary>
    /// Obtém o ranking de turmas de uma determinada escola
    /// </summary>
    Task<IEnumerable<TurmaRankingItemDto>> GetRankingTurmasAsync(Guid? escolaId = null);

    /// <summary>
    /// Obtém o ranking de escolas
    /// </summary>
    Task<IEnumerable<EscolaRankingItemDto>> GetRankingEscolasAsync();

    /// <summary>
    /// Obtém o ranking de alunos de uma turma específica
    /// </summary>
    Task<IEnumerable<TurmaAlunoRankingItemDto>> GetRankingTurmaAlunosAsync(Guid turmaId);
}
