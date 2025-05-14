namespace Application.Modules.Pets.Commands;

public class LikePetCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<LikePetCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(LikePetCommand command, CancellationToken cancellationToken)
    {
        var pet = await _dbContext.Pets
            .AsNoTracking()
            .FilterSoftDeleted()
            .FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken)
            ?? throw new NotFoundException($"Pet with id {command.PetId} not found");

        var like = await _dbContext.PetLikes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.UserId == _dbContext.User!.Id && l.PetId == pet.Id, cancellationToken);

        if (like is not null)
            return pet.Id;

        like = new PetLike
        {
            UserId = _dbContext.User!.Id,
            PetId = pet.Id,
        };

        _dbContext.PetLikes.Add(like);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return pet.Id;
    }
}
