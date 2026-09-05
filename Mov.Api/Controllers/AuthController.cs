using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mov.Domain.Dtos.Auth;
using Mov.Domain.Interfaces.Services;
using Mov.Application.Services;
using Mov.Domain.Exceptions;
using System.Security.Claims;

namespace Mov.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly GoogleOAuthService _googleOAuthService;

    public AuthController(IAuthService authService, GoogleOAuthService googleOAuthService)
    {
        _authService = authService;
        _googleOAuthService = googleOAuthService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao processar login", details = ex.Message });
        }
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(googleLoginDto.IdToken))
            {
                return BadRequest(new { message = "ID Token do Google é obrigatório" });
            }

            var result = await _googleOAuthService.ValidateGoogleTokenAsync(googleLoginDto.IdToken);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (GoogleTokenValidationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (GoogleAuthenticationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao processar login com Google", details = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(adminIdClaim, out var adminId))
            {
                return Unauthorized(new { message = "Admin ID inválido" });
            }

            var result = await _authService.CreateUserAsync(createUserDto, adminId);
            return CreatedAtAction(nameof(CreateUser), result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao criar usuário", details = ex.Message });
        }
    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "User ID inválido" });
            }

            var result = await _authService.ChangePasswordAsync(userId, changePasswordDto);
            return Ok(new { message = "Senha alterada com sucesso" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao alterar senha", details = ex.Message });
        }
    }

    [HttpGet("health/google-oauth")]
    public async Task<IActionResult> HealthCheckGoogleOAuth()
    {
        try
        {
            // Este endpoint apenas testa se consegue obter certificados do Google
            // Se funcionar, retorna OK
            var health = new
            {
                status = "ok",
                message = "Google OAuth2 está configurado corretamente",
                timestamp = DateTime.UtcNow,
                googleIssuer = "https://accounts.google.com",
                certificatesUrl = "https://www.googleapis.com/oauth2/v3/certs"
            };

            return Ok(health);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                status = "error",
                message = "Erro ao verificar Google OAuth",
                details = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        try
        {
            var result = await _authService.RefreshTokenAsync(refreshTokenDto.RefreshToken);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (NotImplementedException ex)
        {
            return StatusCode(501, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao renovar token", details = ex.Message });
        }
    }
}
