namespace Application.Modules.Pets.Commands;

public class AddPetCommandHandler(IApplicationDbContext dbContext, IMediaService mediaService)
    : IRequestHandler<AddPetCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly IMediaService _mediaService = mediaService;

    public async Task<int> Handle(AddPetCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var pet = new Pet
        {
            Name = command.Name!,
            Species = command.Species!.Value,
            Gender = command.Gender!.Value,
            Size = command.Size!.Value,
            Age = command.Age!.Value,
            HasSpecialNeeds = command.HasSpecialNeeds!.Value,
            Description = command.Description,
        };

        if (command.FeaturesIds is not null && command.FeaturesIds.Count > 0)
            pet.Features = await _dbContext.PetFeatures.Where(f => command.FeaturesIds.Contains(f.Id)).ToListAsync(cancellationToken);

        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        if (command.Pictures is not null && command.Pictures.Count > 0)
        {
            var order = 1;
            var pictures = new List<Picture>();
            foreach (var picture in command.Pictures)
            {
                (var url, var path) = await _mediaService.UploadPetPictureAsync(pet.Id, picture);
                pictures.Add(new()
                {
                    Source = FileSource.Internal,
                    Url = url,
                    Path = path,
                    Order = order++,
                });
            }

            pet.Pictures = pictures;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return pet.Id;
    }
}
