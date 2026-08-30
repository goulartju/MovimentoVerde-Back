using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mov.Domain.Interfaces.Services;

namespace Mov.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RankingsController : ControllerBase
{
    private readonly IRankingService _rankingService;

    public RankingsController(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    /// <summary>
    /// Obtém o ranking de alunos
    /// </summary>
    /// <param name="escolaId">ID da escola (opcional). Se fornecido, retorna apenas alunos dessa escola</param>
    [HttpGet("quadro-alunos")]
    public async Task<IActionResult> GetRankingAlunos([FromQuery] Guid? escolaId)
    {
        var resultado = await _rankingService.GetRankingAlunosAsync(escolaId);
        return Ok(resultado);
    }

    /// <summary>
    /// Obtém o ranking de alunos da última semana
    /// </summary>
    /// <param name="escolaId">ID da escola (opcional). Se fornecido, retorna apenas alunos dessa escola</param>
    [HttpGet("semana-alunos")]
    public async Task<IActionResult> GetRankingSemanalAlunos([FromQuery] Guid? escolaId)
    {
        var resultado = await _rankingService.GetRankingSemanalAlunosAsync(escolaId);
        return Ok(resultado);
    }

    /// <summary>
    /// Obtém o ranking de turmas
    /// </summary>
    /// <param name="escolaId">ID da escola (opcional). Se fornecido, retorna apenas turmas dessa escola</param>
    [HttpGet("quadro-turmas")]
    public async Task<IActionResult> GetRankingTurmas([FromQuery] Guid? escolaId)
    {
        var resultado = await _rankingService.GetRankingTurmasAsync(escolaId);
        return Ok(resultado);
    }

    /// <summary>
    /// Obtém o ranking de escolas
    /// </summary>
    [HttpGet("quadro-escolas")]
    public async Task<IActionResult> GetRankingEscolas()
    {
        var resultado = await _rankingService.GetRankingEscolasAsync();
        return Ok(resultado);
    }

    /// <summary>
    /// Obtém o ranking de alunos de uma turma específica
    /// </summary>
    /// <param name="turmaId">ID da turma</param>
    [HttpGet("turmas/{turmaId:guid}/alunos")]
    public async Task<IActionResult> GetRankingTurmaAlunos(Guid turmaId)
    {
        var resultado = await _rankingService.GetRankingTurmaAlunosAsync(turmaId);
        return Ok(resultado);
    }
}
