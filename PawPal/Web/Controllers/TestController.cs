namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("connection/api")]
    public async Task<Result<string>> TestApiConnectionAsync()
        => new("Connected");

    [HttpGet("connection/db")]
    public async Task<Result<IReadOnlyCollection<string>>> TestDbConnectionAsync(CancellationToken cancellationToken)
        => new(await _mediator.Send(new TestQuery(), cancellationToken));

    [HttpGet("exception")]
    public async Task<Result<string>> TestApiExceptionAsync()
        => throw new Exception("Exception occured");

    [HttpGet("auth/anonymous")]
    public async Task<Result<string>> TestAuthAnonymousAsync()
        => new("Anonymous access allowed");

    [HttpGet("auth/authorize")]
    [Auth]
    public async Task<Result<string>> TestAuthAuthorizedAsync()
        => new("Authorized access allowed");

    [HttpGet("auth/admin-authorize")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<string>> TestAuthRoleAuthorizedAsync()
        => new("Admin access allowed");
}
