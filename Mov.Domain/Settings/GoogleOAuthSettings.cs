namespace Mov.Domain.Settings;

public class GoogleOAuthSettings
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Issuer { get; set; } = "https://accounts.google.com";
    public string TokenEndpoint { get; set; } = "https://oauth2.googleapis.com/token";
    public string CertificatesUrl { get; set; } = "https://www.googleapis.com/oauth2/v3/certs";
    public int CertificateCacheDurationMinutes { get; set; } = 60;
}
