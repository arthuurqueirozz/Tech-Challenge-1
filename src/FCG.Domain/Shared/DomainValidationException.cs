namespace FCG.Domain.Shared;

public class DomainValidationException : Exception
{
    public DomainValidationException(string message) : base(message)
    {
    }
}
