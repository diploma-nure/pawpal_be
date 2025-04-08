namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    public async Task<Result<string>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpPost("login")]
    public async Task<Result<string>> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));
}
