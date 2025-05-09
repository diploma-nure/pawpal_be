namespace Application.Modules.Pets.Mappings;

public static class PetMappings
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
            PictureUrl = pet.Pictures?.OrderBy(p => p.Order).FirstOrDefault()?.Url,
        };

    public static PetInListDto ToPetInListDto(this Pet pet, decimal matchPercentage)
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
            PictureUrl = pet.Pictures?.OrderBy(p => p.Order).FirstOrDefault()?.Url,
            MatchPercentage = matchPercentage,
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
            Pictures = pet.Pictures?.OrderBy(p => p.Order).Select(p => p.ToPetPictureDto()).ToList(),
        };

    public static PetPictureDto ToPetPictureDto(this Picture picture)
        => new()
        {
            Id = picture.Id,
            Url = picture.Url,
            Order = picture.Order,
        };

    public static PetShortDto ToPetShortDto(this Pet pet)
        => new()
        {
            Id = pet.Id,
            Name = pet.Name,
            PictureUrl = pet.Pictures?.OrderBy(p => p.Order).FirstOrDefault()?.Url,
        };
}