namespace Application.Modules.Pets.Queries;

public class GetPetByIdQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetPetByIdQuery, PetDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<PetDto> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        var pet = await _dbContext.Pets
            .Include(p => p.Features)
            .Include(p => p.Pictures)
            .AsNoTracking()
            .FilterSoftDeleted()
            .FirstOrDefaultAsync(p => p.Id == query.PetId, cancellationToken)
            ?? throw new NotFoundException($"Pet with id {query.PetId} not found");

        var result = pet.ToPetDto();

        return result;
    }
}
