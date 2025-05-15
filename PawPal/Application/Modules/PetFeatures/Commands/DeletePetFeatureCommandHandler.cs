namespace Application.Modules.PetFeatures.Commands;

public class DeletePetFeatureCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<DeletePetFeatureCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(DeletePetFeatureCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var petFeature = await _dbContext.PetFeatures
            .FirstOrDefaultAsync(p => p.Id == command.PetFeatureId, cancellationToken)
            ?? throw new NotFoundException($"Pet Feature with id {command.PetFeatureId} not found");

        petFeature.SoftDelete();
        await _dbContext.SaveChangesAsync(cancellationToken);

        return petFeature.Id;
    }
}
