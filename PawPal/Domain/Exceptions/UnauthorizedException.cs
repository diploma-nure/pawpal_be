namespace Domain.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException() : base("A001", "Unauthorized")
    {
    }

    public UnauthorizedException(string code, string message) : base(code, message)
    {
    }
}
