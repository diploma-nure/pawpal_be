namespace Application.Modules.Users.Queries;

public class GetLikedPetsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetLikedPetsQuery, List<PetInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<List<PetInListDto>> Handle(GetLikedPetsQuery query, CancellationToken cancellationToken)
    {
        var pets = _dbContext.PetLikes
            .AsNoTracking()
            .Where(l => l.UserId == _dbContext.User!.Id)
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => l.Pet);

        var result = await pets
            .Select(p => p.ToPetInListDto())
            .ToListAsync(cancellationToken);

        return result;
    }
}
