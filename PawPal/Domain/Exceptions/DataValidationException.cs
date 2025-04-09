namespace Domain.Exceptions;

public class DataValidationException(List<string> errors) : Exception("Validation failed")
{
    public List<string> Errors { get; set; } = errors;
}
