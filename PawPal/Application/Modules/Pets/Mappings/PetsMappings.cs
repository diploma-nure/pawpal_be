namespace Application.Modules.Pets.Mappings;

public static class PetsMappings
{
    public static PetInListDto ToPetInListDto(this Pet pet)
        => new()
        {
            Id = pet.Id,
            Name = pet.Name,
            Gender = pet.Gender,
            Size = pet.Size,
            AgeYears = pet.AgeMonths / 12,
            AgeMonths = pet.AgeMonths % 12,
            Breed = pet.Breed ?? Constants.Defaults.PetBreed,
            HasSpecialNeeds = pet.HasSpecialNeeds,
            Features = pet.Features is not null ? JsonSerializer.Deserialize<List<string>>(pet.Features) : null,
            Description = pet.Description,
        };
}