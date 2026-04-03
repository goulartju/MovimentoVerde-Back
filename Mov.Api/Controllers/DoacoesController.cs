using Microsoft.AspNetCore.Mvc;
using Mov.Domain.Dtos.Doacao;
using Mov.Domain.Interfaces.Services;

namespace Mov.Api.Controllers;

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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpGet("matricula/{matriculaId:int}")]
    public async Task<IActionResult> GetByMatriculaId(int matriculaId)
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDoacaoDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDoacaoDto dto)
    {
        if (id != dto.Id) return BadRequest("Id mismatch");

        try
        {
            var result = await _service.UpdateAsync(dto);
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
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
