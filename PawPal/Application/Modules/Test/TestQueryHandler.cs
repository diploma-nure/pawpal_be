namespace Application.Modules.Test;

public class TestQueryHandler : IRequestHandler<TestQuery, IReadOnlyCollection<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public TestQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<string>> Handle(TestQuery query, CancellationToken cancellationToken)
    {
        var result = await _dbContext.TestEntities
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        return result;
    }
}
