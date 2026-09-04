namespace Mov.Domain.Dtos.Auth;

public class GoogleLoginDto
{
    public string IdToken { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
}
