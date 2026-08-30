using Mov.Domain.Dtos.Ranking;
using Mov.Domain.Helpers;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

/// <summary>
/// Serviço para cálculo de rankings
/// </summary>
public class RankingService : IRankingService
{
    private readonly IDoacaoRepository _doacaoRepository;
    private readonly IEscolaRepository _escolaRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMatriculaRepository _matriculaRepository;

    public RankingService(
        IDoacaoRepository doacaoRepository,
        IEscolaRepository escolaRepository,
        ITurmaRepository turmaRepository,
        IMatriculaRepository matriculaRepository)
    {
        _doacaoRepository = doacaoRepository;
        _escolaRepository = escolaRepository;
        _turmaRepository = turmaRepository;
        _matriculaRepository = matriculaRepository;
    }

    /// <summary>
    /// Obtém o ranking de alunos de uma determinada escola
    /// </summary>
    public async Task<IEnumerable<AlunoRankingItemDto>> GetRankingAlunosAsync(Guid? escolaId = null)
    {
        var doacoes = (await _doacaoRepository.GetAllAsync()).ToList();

        // Filtra por escola se fornecido
        if (escolaId.HasValue)
        {
            doacoes = doacoes.Where(d => d.EscolaId == escolaId).ToList();
        }

        // Agrupa por matrícula para obter dados do aluno e turma
        var alunosRanking = doacoes
            .GroupBy(d => new { d.MatriculaId })
            .Select(g =>
            {
                var primeiraDoacao = g.First();
                var matricula = primeiraDoacao.Matricula;
                var turma = matricula?.Turma;
                var escola = primeiraDoacao.Escola;

                return new
                {
                    AlunoId = matricula?.AlunoId,
                    NomeAluno = matricula?.Aluno?.Nome ?? string.Empty,
                    NomeTurma = turma?.Nome ?? string.Empty,
                    NomeEscola = escola?.Nome ?? string.Empty,
                    TotalTampinhas = g.Sum(d => d.QtdTampinha),
                    TotalLacres = g.Sum(d => d.QtdLacre)
                };
            })
            .GroupBy(x => x.AlunoId)
            .Select(g =>
            {
                var primeiro = g.First();
                var totalTampinhas = g.Sum(x => x.TotalTampinhas);
                var totalLacres = g.Sum(x => x.TotalLacres);
                return new
                {
                    primeiro.NomeAluno,
                    primeiro.NomeTurma,
                    primeiro.NomeEscola,
                    TotalTampinhas = totalTampinhas,
                    TotalLacres = totalLacres,
                    Total = totalTampinhas + totalLacres
                };
            })
            .OrderByDescending(x => x.Total)
            .ThenByDescending(x => x.TotalLacres)
            .ThenBy(x => x.NomeAluno)
            .ToList();

        // Atribui posições e medalhas
        var resultado = alunosRanking
            .Select((item, index) => new AlunoRankingItemDto
            {
                Posicao = index + 1,
                Nome = item.NomeAluno,
                QuantidadeTampinhas = item.TotalTampinhas,
                QuantidadeLacres = item.TotalLacres,
                Total = item.Total,
                Medalha = MedalhaHelper.GetMedalha(item.Total),
                Turma = item.NomeTurma,
                Escola = item.NomeEscola
            })
            .ToList();

        return resultado;
    }

    /// <summary>
    /// Obtém o ranking de alunos da última semana de uma determinada escola
    /// </summary>
    public async Task<IEnumerable<AlunoRankingSemanalItemDto>> GetRankingSemanalAlunosAsync(Guid? escolaId = null)
    {
        var doacoes = (await _doacaoRepository.GetAllAsync()).ToList();

        // Filtra pela última semana (últimos 7 dias)
        var dataInicio = DateTime.UtcNow.AddDays(-7);
        doacoes = doacoes.Where(d => d.Data >= dataInicio).ToList();

        // Filtra por escola se fornecido
        if (escolaId.HasValue)
        {
            doacoes = doacoes.Where(d => d.EscolaId == escolaId).ToList();
        }

        // Agrupa por matrícula para obter dados do aluno e turma
        var alunosRanking = doacoes
            .GroupBy(d => new { d.MatriculaId })
            .Select(g =>
            {
                var primeiraDoacao = g.First();
                var matricula = primeiraDoacao.Matricula;
                var turma = matricula?.Turma;
                var escola = primeiraDoacao.Escola;

                return new
                {
                    AlunoId = matricula?.AlunoId,
                    NomeAluno = matricula?.Aluno?.Nome ?? string.Empty,
                    NomeTurma = turma?.Nome ?? string.Empty,
                    NomeEscola = escola?.Nome ?? string.Empty,
                    TotalTampinhas = g.Sum(d => d.QtdTampinha),
                    TotalLacres = g.Sum(d => d.QtdLacre),
                    DataReferencia = DateTime.UtcNow.Date
                };
            })
            .GroupBy(x => x.AlunoId)
            .Select(g =>
            {
                var primeiro = g.First();
                var totalTampinhas = g.Sum(x => x.TotalTampinhas);
                var totalLacres = g.Sum(x => x.TotalLacres);
                return new
                {
                    primeiro.NomeAluno,
                    primeiro.NomeTurma,
                    primeiro.NomeEscola,
                    TotalTampinhas = totalTampinhas,
                    TotalLacres = totalLacres,
                    Total = totalTampinhas + totalLacres,
                    primeiro.DataReferencia
                };
            })
            .OrderByDescending(x => x.Total)
            .ThenByDescending(x => x.TotalLacres)
            .ThenBy(x => x.NomeAluno)
            .ToList();

        // Atribui posições e medalhas
        var resultado = alunosRanking
            .Select((item, index) => new AlunoRankingSemanalItemDto
            {
                Posicao = index + 1,
                Nome = item.NomeAluno,
                QuantidadeTampinhas = item.TotalTampinhas,
                QuantidadeLacres = item.TotalLacres,
                Total = item.Total,
                Medalha = MedalhaHelper.GetMedalha(item.Total),
                Turma = item.NomeTurma,
                Escola = item.NomeEscola,
                DataReferencia = item.DataReferencia.ToString("yyyy-MM-dd"),
                Periodo = $"Últimos 7 dias"
            })
            .ToList();

        return resultado;
    }

    /// <summary>
    /// Obtém o ranking de turmas de uma determinada escola
    /// </summary>
    public async Task<IEnumerable<TurmaRankingItemDto>> GetRankingTurmasAsync(Guid? escolaId = null)
    {
        var doacoes = (await _doacaoRepository.GetAllAsync()).ToList();

        // Filtra por escola se fornecido
        if (escolaId.HasValue)
        {
            doacoes = doacoes.Where(d => d.EscolaId == escolaId).ToList();
        }

        // Agrupa por turma
        var turmasRanking = doacoes
            .GroupBy(d => new { d.Matricula!.TurmaId, TurmaObj = d.Matricula.Turma })
            .Select(g =>
            {
                var turma = g.Key.TurmaObj;
                var escola = turma?.Escola;

                return new
                {
                    TurmaId = turma?.Id,
                    NomeTurma = turma?.Nome ?? string.Empty,
                    EscolaId = escola?.Id,
                    NomeEscola = escola?.Nome ?? string.Empty,
                    TotalTampinhas = g.Sum(d => d.QtdTampinha),
                    TotalLacres = g.Sum(d => d.QtdLacre)
                };
            })
            .OrderByDescending(x => x.TotalTampinhas + x.TotalLacres)
            .ThenByDescending(x => x.TotalLacres)
            .ThenBy(x => x.NomeTurma)
            .ToList();

        // Atribui posições
        var resultado = turmasRanking
            .Select((item, index) => new TurmaRankingItemDto
            {
                Posicao = index + 1,
                Nome = item.NomeTurma,
                QuantidadeTampinhas = item.TotalTampinhas,
                QuantidadeLacres = item.TotalLacres,
                Total = item.TotalTampinhas + item.TotalLacres,
                TurmaId = item.TurmaId.ToString(),
                EscolaId = item.EscolaId.ToString(),
                EscolaNome = item.NomeEscola
            })
            .ToList();

        return resultado;
    }

    /// <summary>
    /// Obtém o ranking de escolas
    /// </summary>
    public async Task<IEnumerable<EscolaRankingItemDto>> GetRankingEscolasAsync()
    {
        var doacoes = (await _doacaoRepository.GetAllAsync()).ToList();

        // Agrupa por escola
        var escolasRanking = doacoes
            .GroupBy(d => d.EscolaId)
            .Select(g =>
            {
                var primeiraDoacao = g.First();
                var escola = primeiraDoacao.Escola;

                return new
                {
                    EscolaId = escola?.Id,
                    NomeEscola = escola?.Nome ?? string.Empty,
                    TotalTampinhas = g.Sum(d => d.QtdTampinha),
                    TotalLacres = g.Sum(d => d.QtdLacre)
                };
            })
            .OrderByDescending(x => x.TotalTampinhas + x.TotalLacres)
            .ThenByDescending(x => x.TotalLacres)
            .ThenBy(x => x.NomeEscola)
            .ToList();

        // Atribui posições
        var resultado = escolasRanking
            .Select((item, index) => new EscolaRankingItemDto
            {
                Posicao = index + 1,
                Nome = item.NomeEscola,
                QuantidadeTampinhas = item.TotalTampinhas,
                QuantidadeLacres = item.TotalLacres,
                Total = item.TotalTampinhas + item.TotalLacres,
                EscolaId = item.EscolaId.ToString()
            })
            .ToList();

        return resultado;
    }

    /// <summary>
    /// Obtém o ranking de alunos de uma turma específica
    /// </summary>
    public async Task<IEnumerable<TurmaAlunoRankingItemDto>> GetRankingTurmaAlunosAsync(Guid turmaId)
    {
        var doacoes = (await _doacaoRepository.GetAllAsync()).ToList();

        // Filtra por turma
        doacoes = doacoes.Where(d => d.Matricula!.TurmaId == turmaId).ToList();

        // Agrupa por aluno
        var alunosRanking = doacoes
            .GroupBy(d => new { d.MatriculaId })
            .Select(g =>
            {
                var primeiraDoacao = g.First();
                var matricula = primeiraDoacao.Matricula;

                return new
                {
                    AlunoId = matricula?.AlunoId,
                    NomeAluno = matricula?.Aluno?.Nome ?? string.Empty,
                    TotalTampinhas = g.Sum(d => d.QtdTampinha),
                    TotalLacres = g.Sum(d => d.QtdLacre)
                };
            })
            .GroupBy(x => x.AlunoId)
            .Select(g =>
            {
                var primeiro = g.First();
                var totalTampinhas = g.Sum(x => x.TotalTampinhas);
                var totalLacres = g.Sum(x => x.TotalLacres);
                return new
                {
                    primeiro.NomeAluno,
                    TotalTampinhas = totalTampinhas,
                    TotalLacres = totalLacres,
                    Total = totalTampinhas + totalLacres
                };
            })
            .OrderByDescending(x => x.Total)
            .ThenByDescending(x => x.TotalLacres)
            .ThenBy(x => x.NomeAluno)
            .ToList();

        // Atribui posições e medalhas
        var resultado = alunosRanking
            .Select((item, index) => new TurmaAlunoRankingItemDto
            {
                Posicao = index + 1,
                Nome = item.NomeAluno,
                QuantidadeTampinhas = item.TotalTampinhas,
                QuantidadeLacres = item.TotalLacres,
                Total = item.Total,
                Medalha = MedalhaHelper.GetMedalha(item.Total)
            })
            .ToList();

        return resultado;
    }
}
