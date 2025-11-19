namespace eMeni.Application.Common.Exceptions;

public sealed class eMeniNotFoundException : Exception
{
    public eMeniNotFoundException(string message) : base(message) { }
}
