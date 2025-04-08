namespace Web.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ConflictException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (UnauthorizedException ex)
        {
            await HandleExceptionAsync(context, ex, StatusCodes.Status401Unauthorized);
        }
        catch (ForbiddenException ex)
        {
            await HandleExceptionAsync(context, ex, StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode = StatusCodes.Status400BadRequest)
    {
        var envelope = new Result<object>(exception.Message, exception.StackTrace);

        var result = JsonSerializer.Serialize(envelope);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(result);
    }
}
