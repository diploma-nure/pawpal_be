namespace Application.Modules.Comments.Queries;

public class GetRecentCommentsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetRecentCommentsQuery, List<CommentInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<List<CommentInListDto>> Handle(GetRecentCommentsQuery query, CancellationToken cancellationToken)
    {
        var comments = await _dbContext.Comments
            .AsNoTracking()
            .Include(c => c.User)
            .OrderByDescending(c => c.UpdatedAt)
            .Take(10)
            .ToListAsync(cancellationToken);

        var result = comments
            .Select(f => f.ToCommentInListDto())
            .ToList();

        return result;
    }
}
