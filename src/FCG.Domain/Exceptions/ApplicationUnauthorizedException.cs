namespace FCG.Domain.Exceptions;

public class ApplicationUnauthorizedException : Exception
{
    public ApplicationUnauthorizedException(string message = "Invalid credentials.") : base(message)
    {
    }
}
