namespace Application.Modules.Pets.Commands;

public class UpdatePetCommandHandler(IApplicationDbContext dbContext, IMediaService mediaService)
    : IRequestHandler<UpdatePetCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly IMediaService _mediaService = mediaService;

    public async Task<int> Handle(UpdatePetCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var pet = await _dbContext.Pets
            .Include(p => p.Features)
            .Include(p => p.Pictures)
            .FilterSoftDeleted()
            .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken)
            ?? throw new NotFoundException($"Pet with id {command.Id} not found");

        if (!string.IsNullOrEmpty(command.Name))
            pet.Name = command.Name;
        if (command.Species.HasValue)
            pet.Species = command.Species.Value;
        if (command.Gender.HasValue)
            pet.Gender = command.Gender.Value;
        if (command.Size.HasValue)
            pet.Size = command.Size.Value;
        if (command.Age.HasValue)
            pet.Age = command.Age.Value;
        if (command.HasSpecialNeeds.HasValue)
            pet.HasSpecialNeeds = command.HasSpecialNeeds.Value;
        if (command.Description is not null)
            pet.Description = command.Description;

        if (command.FeaturesIds is not null && command.FeaturesIds.Count > 0)
            pet.Features = await _dbContext.PetFeatures.Where(f => command.FeaturesIds.Contains(f.Id)).ToListAsync(cancellationToken);
        
        if (command.Pictures is not null && command.Pictures.Count > 0)
        {
            var keptPicturesIds = command.Pictures
                .Where(p => p.Id.HasValue)
                .Select(p => p.Id!.Value)
                .ToList();

            var removedPicturesIds = pet.Pictures
                .Where(p => !keptPicturesIds.Contains(p.Id))
                .ToList();

            foreach (var picture in removedPicturesIds)
            {
                _mediaService.DeletePicture(picture);
                _dbContext.Pictures.Remove(picture);
            }

            var order = 1;
            var updatedPictures = new List<Picture>();
            foreach (var dto in command.Pictures)
            {
                if (dto.Id.HasValue)
                {
                    var existing = pet.Pictures.First(p => p.Id == dto.Id.Value);
                    existing.Order = order++;
                    updatedPictures.Add(existing);
                }
                else if (dto.File is not null)
                {
                    var (url, path) = await _mediaService.UploadPetPictureAsync(pet.Id, dto.File);
                    updatedPictures.Add(new()
                    {
                        Source = FileSource.Internal,
                        Url = url,
                        Path = path,
                        Order = order++,
                    });
                }
                else
                    throw new ConflictException("Each picture entry must have either Id or File");
            }

            pet.Pictures = updatedPictures;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return pet.Id;
    }
}
