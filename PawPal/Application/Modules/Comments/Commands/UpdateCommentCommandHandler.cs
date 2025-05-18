namespace Application.Modules.Comments.Commands;

public class UpdateCommentCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateCommentCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken)
            ?? throw new NotFoundException($"Comment with id {command.Id} not found");

        if (comment.UserId != _dbContext.User!.Id)
            throw new ForbiddenException();

        if (!string.IsNullOrEmpty(command.Comment))
        {
            comment.Value = command.Comment;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return comment.Id;
    }
}
