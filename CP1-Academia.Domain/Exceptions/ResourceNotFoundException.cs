namespace CP1_Academia.Domain.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string resourceName, object id)
        : base($"{resourceName} com id '{id}' não foi encontrado(a).") { }
}