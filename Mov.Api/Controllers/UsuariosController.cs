using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mov.Domain.Dtos.Usuario;
using Mov.Domain.Enums;
using Mov.Domain.Interfaces.Services;

namespace Mov.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound(new { sucesso = false, mensagem = "Usuário não encontrado" });
        return Ok(new { sucesso = true, dados = item });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                new { sucesso = true, dados = result });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new 
            { 
                sucesso = false, 
                mensagem = "Validação falhou",
                erros = ex.Errors.Select(e => e.ErrorMessage)
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { sucesso = false, mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { sucesso = false, mensagem = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new 
            { 
                sucesso = false, 
                mensagem = "Validação falhou",
                erros = ex.Errors.Select(e => e.ErrorMessage)
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { sucesso = false, mensagem = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok(new { sucesso = true, mensagem = "Usuário deletado com sucesso" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { sucesso = false, mensagem = ex.Message });
        }
    }

    // Endpoint para retornar as permissões disponíveis
    [HttpGet("permissoes")]
    public IActionResult GetPermissoes()
    {
        var permissoes = Enum.GetValues(typeof(PermissaoEnum))
            .Cast<PermissaoEnum>()
            .Select(p => new { id = (int)p, nome = p.ToString() })
            .ToList();

        return Ok(new { sucesso = true, dados = permissoes });
    }
}
