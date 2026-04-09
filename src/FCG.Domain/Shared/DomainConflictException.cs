namespace FCG.Domain.Shared;

public class DomainConflictException : Exception
{
    public DomainConflictException(string message) : base(message)
    {
    }
}
