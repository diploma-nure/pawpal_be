namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly IApplicationDbContext _dbContext;

    public TestController(ILogger<TestController> logger, IApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("api-connection")]
    public async Task<ActionResult<string>> TestApiConnection()
    {
        return Ok("Connected");
    }

    [HttpGet("db-connection")]
    public async Task<ActionResult<List<TestEntity>>> TestDbConnection(CancellationToken cancellationToken)
    {
        return Ok(await _dbContext.TestEntities.ToListAsync(cancellationToken));
    }
}
