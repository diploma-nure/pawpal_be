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
}
