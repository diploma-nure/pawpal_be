namespace Application.Modules.Pets.Commands;

public class DeletePetCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<DeletePetCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(DeletePetCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var pet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken)
            ?? throw new NotFoundException($"Pet with id {command.PetId} not found");

        _dbContext.Pets.Remove(pet);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return pet.Id;
    }
}
