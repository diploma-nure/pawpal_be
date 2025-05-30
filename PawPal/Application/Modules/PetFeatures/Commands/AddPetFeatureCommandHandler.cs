namespace Application.Modules.PetFeatures.Commands;

public class AddPetFeatureCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<AddPetFeatureCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(AddPetFeatureCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        if (_dbContext.PetFeatures.Any(f => f.Feature == command.Feature))
            throw new ConflictException(Constants.ResponseCodes.NotFoundPetFeature, $"Pet Feature {command.Feature} already exists");


        var petFeature = new PetFeature
        {
            Feature = command.Feature,
        };

        _dbContext.PetFeatures.Add(petFeature);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return petFeature.Id;
    }
}
