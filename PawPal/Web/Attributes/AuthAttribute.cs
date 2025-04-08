namespace Web.Attributes;

public class AuthAttribute(string[]? roles = null) : ActionFilterAttribute
{
    private readonly List<Role>? _roles = roles?.Select(r => Enum.TryParse<Role>(r, out var role) ? role : Role.None).ToList();

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();

        var user = dbContext.User;
        if (user == null)
            throw new UnauthorizedException("Unauthorized");

        if (_roles != null && _roles.Count > 0)
        {
            if (!_roles.Contains(user.Role))
                throw new ForbiddenException("Forbidden");
        }

        await next();
    }
}
