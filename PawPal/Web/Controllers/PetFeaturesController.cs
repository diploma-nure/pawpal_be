namespace Web.Controllers;

[ApiController]
[Route("api/pet-features")]
public class PetFeaturesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<Result<List<PetFeatureDto>>> GetPetFeaturesAsync(CancellationToken cancellationToken)
        => new(await _mediator.Send(new GetPetFeaturesQuery(), cancellationToken));
}
