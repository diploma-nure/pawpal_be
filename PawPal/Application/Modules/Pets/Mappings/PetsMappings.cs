namespace Application.Modules.Pets.Mappings;

public static class PetsMappings
{
    public static PetInListDto ToPetInListDto(this Pet pet)
        => new()
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = pet.Species,
            Gender = pet.Gender,
            Size = pet.Size,
            Age = pet.Age,
            HasSpecialNeeds = pet.HasSpecialNeeds,
            Description = pet.Description,
            PictureUrl = pet.PicturesUrls?.FirstOrDefault(),
        };

    public static PetDto ToPetDto(this Pet pet)
        => new()
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = pet.Species,
            Gender = pet.Gender,
            Size = pet.Size,
            Age = pet.Age,
            HasSpecialNeeds = pet.HasSpecialNeeds,
            Features = pet.Features.Select(f => f.Feature).ToList(),
            Description = pet.Description,
            PicturesUrls = pet.PicturesUrls,
        };
}