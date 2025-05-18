namespace Application.Modules.Comments.Commands;

public class DeleteCommentCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<DeleteCommentCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(p => p.Id == command.CommentId, cancellationToken)
            ?? throw new NotFoundException($"Comment with id {command.CommentId} not found");

        if (comment.UserId != _dbContext.User!.Id && _dbContext.User.Role is not Role.Admin)
            throw new ForbiddenException();

        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
