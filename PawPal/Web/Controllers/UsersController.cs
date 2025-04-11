namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("info")]
    [Auth]
    public async Task<Result<UserInfoDto>> GetUserInfoAsync([FromQuery] GetUserInfoQuery query, CancellationToken cancellationToken)
        => new(await _mediator.Send(query, cancellationToken));

    [HttpGet("pets/liked")]
    [Auth]
    public async Task<Result<List<PetInListDto>>> GetLikedPetsAsync(CancellationToken cancellationToken)
        => new(await _mediator.Send(new GetLikedPetsQuery(), cancellationToken));
}
