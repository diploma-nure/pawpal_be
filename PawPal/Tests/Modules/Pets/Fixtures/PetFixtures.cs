namespace Tests.Modules.Pets.Fixtures;

public static class PetFixtures
{
    public static Pet FakePetEntity(int id)
    {
        var faker = new Faker<Pet>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => id)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Species, f => f.PickRandom<PetSpecies>())
            .RuleFor(x => x.Gender, f => f.PickRandom<PetGender>())
            .RuleFor(x => x.Size, f => f.PickRandom<PetSize>())
            .RuleFor(x => x.Age, f => f.PickRandom<PetAge>())
            .RuleFor(x => x.HasSpecialNeeds, f => f.Random.Bool())
            .RuleFor(x => x.Features, f => [])
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .RuleFor(x => x.PetLikes, f => [])
            .RuleFor(x => x.Pictures, f => [])
            .RuleFor(x => x.Applications, f => [])
            .RuleFor(x => x.Comments, f => [])
            .RuleFor(x => x.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.DeletedAt, f => null);

        faker.Validate();

        return faker.Generate();
    }
}
