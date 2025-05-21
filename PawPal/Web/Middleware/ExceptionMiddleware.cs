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
            await HandleExceptionAsync(context, ex.Message, ex);
        }
        catch (UnauthorizedException ex)
        {
            await HandleExceptionAsync(context, ex.Message, ex, StatusCodes.Status401Unauthorized);
        }
        catch (ForbiddenException ex)
        {
            await HandleExceptionAsync(context, ex.Message, ex, StatusCodes.Status403Forbidden);
        }
        catch (DataValidationException ex)
        {
            await HandleExceptionAsync(context, ex.Message, ex, errors: ex.Errors);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex.Message, ex, StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex.Message, ex, StatusCodes.Status500InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string message, Exception exception, int statusCode = StatusCodes.Status400BadRequest, List<string>? errors = null)
    {
        if (errors is null && exception.InnerException is not null)
            errors = [exception.InnerException.Message];

        var envelope = new Result<object>(message, errors);
        var result = JsonSerializer.Serialize(envelope);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(result);
    }
}
