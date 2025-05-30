namespace Domain.Exceptions;

public class DataValidationException(List<string> errors) : BaseException("V001", "Validation failed")
{
    public List<string> Errors { get; set; } = errors;
}
