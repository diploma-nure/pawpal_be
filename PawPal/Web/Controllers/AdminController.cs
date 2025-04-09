namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> RegisterAsync(RegisterAdminCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));
}
