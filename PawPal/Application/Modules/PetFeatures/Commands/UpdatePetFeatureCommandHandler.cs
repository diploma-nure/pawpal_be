namespace Application.Modules.PetFeatures.Commands;

public class UpdatePetFeatureCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdatePetFeatureCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(UpdatePetFeatureCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var petFeature = await _dbContext.PetFeatures
            .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundPetFeature, $"Pet Feature with id {command.Id} not found");

        if (!string.IsNullOrEmpty(command.Feature))
        {
            petFeature.Feature = command.Feature;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return petFeature.Id;
    }
}
