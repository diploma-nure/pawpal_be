namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("add")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> AddPetAsync([FromForm] AddPetCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpGet("filtered")]
    public async Task<Result<PaginatedListDto<PetInListDto>>> GetPetsFilteredAsync([FromQuery] GetPetsFilteredQuery query, CancellationToken cancellationToken)
        => new(await _mediator.Send(query, cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<Result<PetDto>> GetPetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        => new(await _mediator.Send(new GetPetByIdQuery(id), cancellationToken));

    [HttpPatch("like/{id:int}")]
    [Auth]
    public async Task<Result<int>> LikePetAsync([FromRoute] int id, CancellationToken cancellationToken)
        => new(await _mediator.Send(new LikePetCommand(id), cancellationToken));

    [HttpPatch("unlike/{id:int}")]
    [Auth]
    public async Task<Result<int>> UnlikePetAsync([FromRoute] int id, CancellationToken cancellationToken)
        => new(await _mediator.Send(new UnlikePetCommand(id), cancellationToken));
}
