namespace Application.Modules.Comments.Commands;

public class AddCommentCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<AddCommentCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(AddCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            Value = command.Comment,
            PetId = command.PetId,
            UserId = _dbContext.User!.Id,
        };

        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
