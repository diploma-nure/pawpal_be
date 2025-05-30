namespace Domain.Exceptions;

public class ConflictException(string code,string message) : BaseException(code, message)
{
}
