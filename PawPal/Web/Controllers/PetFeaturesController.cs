namespace Web.Controllers;

[ApiController]
[Route("api/pet-features")]
public class PetFeaturesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<Result<List<PetFeatureInListDto>>> GetPetFeaturesAsync(CancellationToken cancellationToken)
        => new(await _mediator.Send(new GetPetFeaturesQuery(), cancellationToken));

    [HttpPost("add")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> AddPetFeatureAsync([FromForm] AddPetFeatureCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpPatch("update")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> UpdatePetFeatureAsync([FromForm] UpdatePetFeatureCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpDelete("{petFeatureId:int}")]
    [Auth([Constants.Roles.Admin])]
    public async Task<Result<int>> DeletePetFeatureAsync([FromRoute] int petFeatureId, CancellationToken cancellationToken)
        => new(await _mediator.Send(new DeletePetFeatureCommand(petFeatureId), cancellationToken));
}
