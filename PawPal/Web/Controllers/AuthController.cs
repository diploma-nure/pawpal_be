namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("login")]
    public async Task<Result<LoginResponseDto>> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpPost("login/google")]
    public async Task<Result<LoginResponseDto>> GoogleLoginAsync([FromBody] GoogleLoginCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));
}
