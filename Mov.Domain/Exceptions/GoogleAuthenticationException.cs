namespace Mov.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando há erro na autenticação Google
/// </summary>
public class GoogleAuthenticationException : Exception
{
    public GoogleAuthenticationException(string message) : base(message)
    {
    }

    public GoogleAuthenticationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exceção lançada quando o token Google é inválido ou expirou
/// </summary>
public class GoogleTokenValidationException : GoogleAuthenticationException
{
    public GoogleTokenValidationException(string message) : base(message)
    {
    }

    public GoogleTokenValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
