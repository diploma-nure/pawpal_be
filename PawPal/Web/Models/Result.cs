namespace Web.Models;

public class Result<TData>
{
    public TData? Data { get; set; }

    public string? Error { get; set; }

    public string? ErrorDescription { get; set; }

    public Result(TData data)
    {
        Data = data;
    }

    public Result(string error, string? errorDescription)
    {
        Error = error;
        ErrorDescription = errorDescription;
    }
}
