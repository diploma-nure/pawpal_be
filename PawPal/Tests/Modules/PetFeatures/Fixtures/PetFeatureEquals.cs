namespace Tests.Modules.PetFeatures.Fixtures;

public static class PetFeatureEquals
{
    public static void EqualTo(this PetFeature entity, PetFeatureInListDto dto)
    {
        entity.Id.Should().Be(dto.Id);
        entity.Feature.Should().Be(dto.Feature);
    }

    public static void EqualTo(this PetFeature entity, AddPetFeatureCommand command)
    {
        entity.Feature.Should().Be(command.Feature);
    }

    public static void EqualTo(this PetFeature entity, UpdatePetFeatureCommand command)
    {
        entity.Feature.Should().Be(command.Feature);
    }
}
