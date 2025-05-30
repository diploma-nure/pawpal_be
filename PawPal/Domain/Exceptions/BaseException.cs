namespace Domain.Exceptions;

public class BaseException(string code, string message) : Exception(message)
{
    public string Code { get; set; } = code;
}
