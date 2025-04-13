namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SurveysController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Auth]
    public async Task<Result<SurveyDto>> GetSurveyAsync([FromQuery] GetSurveyQuery query, CancellationToken cancellationToken)
        => new(await _mediator.Send(query, cancellationToken));

    [HttpPut("complete")]
    [Auth]
    public async Task<Result<int>> CompleteSurveyAsync([FromBody] CompleteSurveyCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));
}
