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

    [HttpPost("password/recovery/send-code")]
    public async Task<Result<int>> SendPasswordRecoveryCodeAsync([FromBody] SendPasswordRecoveryCodeCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpPost("password/recovery/validate-code")]
    public async Task<Result<int>> ValidatePasswordRecoveryCodeAsync([FromBody] ValidatePasswordRecoveryCodeCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpPatch("password/change")]
    public async Task<Result<int>> ChangePasswordAsync([FromBody] ChangeUserPasswordCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));
}
