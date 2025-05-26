namespace Tests.Modules.PetFeatures.Fixtures;

public static class PetFeatureFixtures
{
    public static PetFeature FakePetFeatureEntity(
        int id,
        string? feature = null,
        List<SurveyPetPreferences>? surveysPetPreferences = null,
        DateTime? deletedAt = null)
    {
        var faker = new Faker<PetFeature>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => id)
            .RuleFor(x => x.Feature, f => feature ?? f.Lorem.Word())
            .RuleFor(x => x.SurveysPetPreferences, f => surveysPetPreferences ?? [])
            .RuleFor(x => x.Pets, f => [])
            .RuleFor(x => x.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.DeletedAt, f => deletedAt);

        faker.Validate();

        return faker.Generate();
    }
    public static AddPetFeatureCommand FakeAddPetFeatureCommand()
    {
        var faker = new Faker<AddPetFeatureCommand>()
            .StrictMode(true)
            .RuleFor(x => x.Feature, f => f.Lorem.Word());

        faker.Validate();

        return faker.Generate();
    }

    public static UpdatePetFeatureCommand FakeUpdatePetFeatureCommand(int featureId)
    {
        var faker = new Faker<UpdatePetFeatureCommand>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => featureId)
            .RuleFor(x => x.Feature, f => f.Lorem.Word());

        faker.Validate();

        return faker.Generate();
    }
}
