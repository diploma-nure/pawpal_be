namespace Domain.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Action forbidden")
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }
}
