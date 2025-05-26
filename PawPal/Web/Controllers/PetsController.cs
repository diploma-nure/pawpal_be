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

    [HttpGet("recommended")]
    [Auth]
    public async Task<Result<PaginatedListDto<PetInListDto>>> GetRecommendedPetsAsync([FromQuery] GetRecommendedPetsQuery query, CancellationToken cancellationToken)
        => new(await _mediator.Send(query, cancellationToken));

    [HttpGet("{petId:int}")]
    public async Task<Result<PetDto>> GetPetByIdAsync([FromRoute] int petId, CancellationToken cancellationToken)
        => new(await _mediator.Send(new GetPetByIdQuery(petId), cancellationToken));

    [HttpPatch("update")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> UpdatePetAsync([FromForm] UpdatePetCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpDelete("{petId:int}")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> DeletePetAsync([FromRoute] int petId, CancellationToken cancellationToken)
        => new(await _mediator.Send(new DeletePetCommand(petId), cancellationToken));

    [HttpPatch("like/{petId:int}")]
    [Auth]
    public async Task<Result<int>> LikePetAsync([FromRoute] int petId, CancellationToken cancellationToken)
        => new(await _mediator.Send(new LikePetCommand(petId), cancellationToken));

    [HttpPatch("unlike/{petId:int}")]
    [Auth]
    public async Task<Result<int>> UnlikePetAsync([FromRoute] int petId, CancellationToken cancellationToken)
        => new(await _mediator.Send(new UnlikePetCommand(petId), cancellationToken));
}
