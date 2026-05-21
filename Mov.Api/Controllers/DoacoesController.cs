using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mov.Domain.Dtos.Doacao;
using Mov.Domain.Interfaces.Services;

namespace Mov.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DoacoesController : ControllerBase
{
    private readonly IDoacaoService _service;

    public DoacoesController(IDoacaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpGet("matricula/{matriculaId:Guid}")]
    public async Task<IActionResult> GetByMatriculaId(Guid matriculaId)
    {
        var items = await _service.GetByMatriculaIdAsync(matriculaId);
        return Ok(items);
    }

    [HttpGet("escola/{escolaId:guid}")]
    public async Task<IActionResult> GetByEscolaId(Guid escolaId)
    {
        var items = await _service.GetByEscolaIdAsync(escolaId);
        return Ok(items);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetByFilter([FromQuery] DoacaoFilterDto filter)
    {
        try
        {
            var items = await _service.GetByFilterAsync(filter);
            return Ok(items);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpPost("lote")]
    public async Task<IActionResult> CreateLote([FromBody] CreateDoacaoLoteDto dto)
    {
        try
        {
            var result = await _service.CreateLoteAsync(dto);
            return CreatedAtAction(nameof(GetById), result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpPost("filter")]
    public async Task<IActionResult> CreateByFilter([FromBody] DoacaoFilterDto filter)
    {
        try
        {
            var result = await _service.CreateByFilterAsync(filter);
            return Ok(result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpPut("lote")]
    public async Task<IActionResult> UpdateLote([FromBody] UpdateDoacaoLoteDto dto)
    {

        try
        {
            var result = await _service.UpdateLoteAsync(dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
