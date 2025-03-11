namespace Web.Middleware;

public class UnhandledExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public UnhandledExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var envelope = new Result<object>(exception.Message, exception.StackTrace);

        var result = JsonSerializer.Serialize(envelope);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(result);
    }
}
