namespace CP1_Academia.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) {}
}