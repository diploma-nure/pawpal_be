namespace Web.Models;

public class Result<TData>
{
    public TData? Data { get; set; }

    public string? Code { get; set; }

    public string? Message { get; set; }

    public List<string>? Errors { get; set; }

    public Result(TData data)
    {
        Data = data;
        Message = Constants.Defaults.SuccessMessage;
        Code = Constants.ResponseCodes.Success;
    }

    public Result(string code, string message, List<string>? errors)
    {
        Code = code;
        Message = message;
        Errors = errors;
    }
}
