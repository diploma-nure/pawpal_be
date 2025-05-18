namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("recent")]
    public async Task<Result<List<CommentInListDto>>> GetCommentsAsync(CancellationToken cancellationToken)
        => new(await _mediator.Send(new GetRecentCommentsQuery(), cancellationToken));

    [HttpPost("add")]
    [Auth]
    public async Task<Result<int>> AddCommentAsync([FromForm] AddCommentCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpPatch("update")]
    [Auth]
    public async Task<Result<int>> UpdateCommentAsync([FromForm] UpdateCommentCommand command, CancellationToken cancellationToken)
        => new(await _mediator.Send(command, cancellationToken));

    [HttpDelete("{commentId:int}")]
    [Auth]
    public async Task<Result<int>> DeleteCommentAsync([FromRoute] int commentId, CancellationToken cancellationToken)
        => new(await _mediator.Send(new DeleteCommentCommand(commentId), cancellationToken));
}
