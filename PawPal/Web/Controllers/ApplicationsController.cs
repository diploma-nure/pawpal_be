namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("filtered")]
    [Auth]
    public async Task<Result<PaginatedListDto<ApplicationInListDto>>> GetApplicationsFilteredAsync([FromQuery] GetApplicationsFilteredQuery query, CancellationToken cancellationToken)
        => new(await _mediator.Send(query, cancellationToken));

    [HttpPost("submit")]
    [Auth([Constants.Roles.User])]
    public async Task<Result<int>> SubmitApplicationAsync([FromBody] SubmitApplicationCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpPatch("status")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> ChangeApplicationStatusAsync([FromBody] ChangeApplicationStatusCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));
}
