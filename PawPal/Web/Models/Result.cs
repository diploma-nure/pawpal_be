namespace Web.Models;

public class Result<TData>
{
    public TData? Data { get; set; }

    public string? Message { get; set; }

    public List<string>? Errors { get; set; }

    public Result(TData data)
    {
        Data = data;
        Message = Constants.Defaults.SuccessMessage;
    }

    public Result(string message, List<string>? errors)
    {
        Message = message;
        Errors = errors;
    }
}
