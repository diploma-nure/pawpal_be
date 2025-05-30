namespace Domain.Exceptions;

public class NotFoundException(string code, string message) : BaseException(code, message)
{
}
