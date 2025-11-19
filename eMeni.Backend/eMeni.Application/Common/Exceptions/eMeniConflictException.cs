namespace eMeni.Application.Common.Exceptions;

public sealed class eMeniConflictException : Exception
{
    public eMeniConflictException(string message) : base(message) { }
}
