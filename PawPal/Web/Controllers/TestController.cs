namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api-connection")]
    public async Task<Result<string>> TestApiConnection()
        => new("Connected");

    [HttpGet("db-connection")]
    public async Task<Result<IReadOnlyCollection<string>>> TestDbConnection(CancellationToken cancellationToken)
        => new(await _mediator.Send(new TestQuery(), cancellationToken));

    [HttpGet("api-exception")]
    public async Task<Result<string>> TestApiException()
        => throw new Exception("Exception occured");
}
