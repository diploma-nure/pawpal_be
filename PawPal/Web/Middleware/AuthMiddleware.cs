namespace Web.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context, ITokenService tokenService, IApplicationDbContext dbContext)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        var userId = await tokenService.ValidateToken(token);

        if (userId != null)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId.Value)
                ?? throw new UnauthorizedException($"User with id {userId.Value} was not found");

            dbContext.User = user;
        }

        await _next(context);
    }
}
