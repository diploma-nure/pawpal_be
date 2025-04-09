namespace Application.Modules.Pets.Commands;

public class AddPetCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<AddPetCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(AddPetCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var pet = new Pet
        {
            Name = command.Name!,
            Gender = command.Gender!.Value,
            Size = command.Size!.Value,
            AgeMonths = command.AgeMonths!.Value + command.AgeYears!.Value * 12,
            Breed = command.Breed,
            HasSpecialNeeds = command.HasSpecialNeeds!.Value,
            Features = command.Features is not null ? JsonSerializer.Serialize(command.Features) : null,
            Description = command.Description,
        };

        _dbContext.Pets.Add(pet);
        await _dbContext.SaveShangesAsync(cancellationToken);

        return pet.Id;
    }
}
