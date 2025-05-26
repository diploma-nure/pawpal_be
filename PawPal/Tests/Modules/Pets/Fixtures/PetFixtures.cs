namespace Tests.Modules.Pets.Fixtures;

public static class PetFixtures
{
    public static Pet FakePetEntity(
        int id,
        string? name = null,
        PetSpecies? species = null,
        PetGender? gender = null,
        PetSize? size = null,
        PetAge? age = null,
        bool? hasSpecialNeeds = null,
        DateTime? createdAt = null)
    {
        var faker = new Faker<Pet>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => id)
            .RuleFor(x => x.Name, f => name ?? f.Name.FirstName())
            .RuleFor(x => x.Species, f => species ?? f.PickRandom<PetSpecies>())
            .RuleFor(x => x.Gender, f => gender ?? f.PickRandom<PetGender>())
            .RuleFor(x => x.Size, f => size ?? f.PickRandom<PetSize>())
            .RuleFor(x => x.Age, f => age ?? f.PickRandom<PetAge>())
            .RuleFor(x => x.HasSpecialNeeds, f => hasSpecialNeeds ?? f.Random.Bool())
            .RuleFor(x => x.Features, f => [])
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .RuleFor(x => x.PetLikes, f => [])
            .RuleFor(x => x.Pictures, f => [])
            .RuleFor(x => x.Applications, f => [])
            .RuleFor(x => x.Comments, f => [])
            .RuleFor(x => x.CreatedAt, f => createdAt ?? DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.DeletedAt, f => null);

        faker.Validate();

        return faker.Generate();
    }

    public static AddPetCommand FakeAddPetCommand()
    {
        var faker = new Faker<AddPetCommand>()
            .StrictMode(true)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Species, f => f.PickRandom<PetSpecies>())
            .RuleFor(x => x.Gender, f => f.PickRandom<PetGender>())
            .RuleFor(x => x.Size, f => f.PickRandom<PetSize>())
            .RuleFor(x => x.Age, f => f.PickRandom<PetAge>())
            .RuleFor(x => x.HasSpecialNeeds, f => f.Random.Bool())
            .RuleFor(x => x.FeaturesIds, f => [])
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .RuleFor(x => x.Pictures, f => []);

        faker.Validate();

        return faker.Generate();
    }

    public static UpdatePetCommand FakeUpdatePetCommand(int petId)
    {
        var faker = new Faker<UpdatePetCommand>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => petId)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Species, f => f.PickRandom<PetSpecies>())
            .RuleFor(x => x.Gender, f => f.PickRandom<PetGender>())
            .RuleFor(x => x.Size, f => f.PickRandom<PetSize>())
            .RuleFor(x => x.Age, f => f.PickRandom<PetAge>())
            .RuleFor(x => x.HasSpecialNeeds, f => f.Random.Bool())
            .RuleFor(x => x.FeaturesIds, f => [])
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .RuleFor(x => x.Pictures, f => []);

        faker.Validate();

        return faker.Generate();
    }

    public static GetPetsFilteredQuery FakeGetPetsFilteredQuery(
        List<PetSpecies>? species = null,
        List<PetGender>? genders = null,
        List<PetSize>? sizes = null,
        List<PetAge>? ages = null,
        bool? hasSpecialNeeds = null,
        int page = 1,
        int pageSize = 10,
        PetSortingOptions sortBy = PetSortingOptions.Name,
        SortingDirection sortDirection = SortingDirection.Asc)
    {
        var faker = new Faker<GetPetsFilteredQuery>()
            .StrictMode(true)
            .RuleFor(x => x.Species, f => species)
            .RuleFor(x => x.Genders, f => genders)
            .RuleFor(x => x.Sizes, f => sizes)
            .RuleFor(x => x.Ages, f => ages)
            .RuleFor(x => x.HasSpecialNeeds, f => hasSpecialNeeds)
            .RuleFor(x => x.Pagination, f => new PaginationDto() { Page = page, PageSize = pageSize })
            .RuleFor(x => x.Sorting, f => new SortingDto<PetSortingOptions>() { Type = sortBy, Direction = sortDirection });

        faker.Validate();

        return faker.Generate();
    }
}
