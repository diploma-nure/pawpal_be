namespace Application.Modules.Pets.Commands;

public class UnlikePetCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UnlikePetCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(UnlikePetCommand command, CancellationToken cancellationToken)
    {
        var pet = await _dbContext.Pets
            .AsNoTracking()
            .FilterSoftDeleted()
            .FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundPet, $"Pet with id {command.PetId} not found");

        var like = await _dbContext.PetLikes
            .FirstOrDefaultAsync(l => l.UserId == _dbContext.User!.Id && l.PetId == pet.Id, cancellationToken);

        if (like is null)
            return pet.Id;

        _dbContext.PetLikes.Remove(like);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return pet.Id;
    }
}
