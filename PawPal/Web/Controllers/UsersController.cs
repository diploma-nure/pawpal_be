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

    [HttpPut("info/update")]
    [Auth]
    public async Task<Result<int>> UpdateUserInfoAsync([FromBody] UpdateUserInfoCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpGet("pets/liked")]
    [Auth]
    public async Task<Result<List<PetInListDto>>> GetLikedPetsAsync(CancellationToken cancellationToken)
        => new(await _mediator.Send(new GetLikedPetsQuery(), cancellationToken));
}
