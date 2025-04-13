namespace Application.Modules.PetFeatures.Mappings;

public static class PetFeatureMappings
{
    public static PetFeatureDto ToPetInListDto(this PetFeature feature)
        => new()
        {
            Id = feature.Id,
            Feature = feature.Feature,
        };
}