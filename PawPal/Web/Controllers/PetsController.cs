namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("add")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> RegisterAsync(AddPetCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpGet("filtered")]
    public async Task<Result<List<PetInListDto>>> LoginAsync([FromQuery] GetPetsFilteredQuery command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));
}
