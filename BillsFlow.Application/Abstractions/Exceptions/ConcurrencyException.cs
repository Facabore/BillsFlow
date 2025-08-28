namespace BillsFlow.Application.Abstractions.Exceptions;

public sealed class ConcurrencyException : Exception
{
    public ConcurrencyException(string meessage, Exception innerException) 
        : base(meessage, innerException)
    {
    }
}