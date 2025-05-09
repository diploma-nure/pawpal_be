namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("slots")]
    [Auth]
    public async Task<Result<List<DaySlotDto>>> GetMeetingSlotsAsync([FromQuery] GetMeetingSlotsQuery query, CancellationToken cancellationToken)
        => new(await _mediator.Send(query, cancellationToken));

    [HttpPost("schedule")]
    [Auth([Constants.Roles.User])]
    public async Task<Result<int>> ScheduleMeetingAsync([FromBody] ScheduleMeetingCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpGet("join")]
    [Auth]
    public async Task<Result<MeetingJoinInfoDto>> JoinMeetingAsync([FromQuery] JoinMeetingCommand command, CancellationToken cancellationToken)
    => new(await _mediator.Send(command, cancellationToken));
}
