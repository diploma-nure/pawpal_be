namespace Tests.Modules.Pets.Queries;

public class GetPetsFilteredQueryHandlerTests : HandlerTestsBase
{
    private GetPetsFilteredQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new GetPetsFilteredQueryHandler(_dbContext);
    }

    [Test]
    public async Task WhenNoFilters_ShouldReturnAllPets_SortedByNameAsc()
    {
        // Arrange
        var pet1 = PetFixtures.FakePetEntity(1, name: "aaa");
        var pet2 = PetFixtures.FakePetEntity(2, name: "bbb");
        _dbContext.Pets.AddRange(pet1, pet2);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = PetFixtures.FakeGetPetsFilteredQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(2);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.Items.Select(x => x.Id).Should().Equal(pet1.Id, pet2.Id);
    }

    [Test]
    [TestCase(new PetSpecies[] { PetSpecies.Cat })]
    [TestCase(new PetSpecies[] { PetSpecies.Dog })]
    [TestCase(new PetSpecies[] { PetSpecies.Cat, PetSpecies.Dog })]
    public async Task WhenFilterBySpecies_ShouldReturnOnlyThatSpecies(PetSpecies[] species)
    {
        // Arrange
        var petCat = PetFixtures.FakePetEntity(1, species: PetSpecies.Cat);
        var petDog = PetFixtures.FakePetEntity(2, species: PetSpecies.Dog);
        _dbContext.Pets.AddRange(petCat, petDog);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = PetFixtures.FakeGetPetsFilteredQuery(species: species.ToList());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(species.Length);
        foreach (var item in result.Items)
            species.Should().Contain(item.Species);
    }

    [Test]
    [TestCase(new PetSize[] { PetSize.Small })]
    [TestCase(new PetSize[] { PetSize.Medium })]
    [TestCase(new PetSize[] { PetSize.Large })]
    [TestCase(new PetSize[] { PetSize.Small, PetSize.Medium, PetSize.Large })]
    public async Task WhenFilterBySize_ShouldReturnOnlyThatSize(PetSize[] sizes)
    {
        // Arrange
        var pet1 = PetFixtures.FakePetEntity(1, size: PetSize.Small);
        var pet2 = PetFixtures.FakePetEntity(2, size: PetSize.Medium);
        var pet3 = PetFixtures.FakePetEntity(3, size: PetSize.Large);
        _dbContext.Pets.AddRange(pet1, pet2, pet3);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = PetFixtures.FakeGetPetsFilteredQuery(sizes: sizes.ToList());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(sizes.Length);
        foreach (var item in result.Items)
            sizes.Should().Contain(item.Size);
    }

    [Test]
    [TestCase(new PetGender[] { PetGender.Male })]
    [TestCase(new PetGender[] { PetGender.Female })]
    [TestCase(new PetGender[] { PetGender.Male, PetGender.Female })]
    public async Task WhenFilterByGender_ShouldReturnOnlyThatGender(PetGender[] genders)
    {
        // Arrange
        var petMale = PetFixtures.FakePetEntity(1, gender: PetGender.Male);
        var petFemale = PetFixtures.FakePetEntity(2, gender: PetGender.Female);
        _dbContext.Pets.AddRange(petMale, petFemale);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = PetFixtures.FakeGetPetsFilteredQuery(genders: genders.ToList());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(genders.Length);
        foreach (var item in result.Items)
            genders.Should().Contain(item.Gender);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task WhenFilterBySpecialNeeds_ShouldReturnOnlyThatPets(bool hasSpecialNeeds)
    {
        // Arrange
        var pet1 = PetFixtures.FakePetEntity(1, hasSpecialNeeds: true);
        var pet2 = PetFixtures.FakePetEntity(2, hasSpecialNeeds: false);
        _dbContext.Pets.AddRange(pet1, pet2);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = PetFixtures.FakeGetPetsFilteredQuery(hasSpecialNeeds: hasSpecialNeeds);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(1);
        result.Items.Single().HasSpecialNeeds.Should().Be(hasSpecialNeeds);
    }

    [Test]
    [TestCase(new PetAge[] { PetAge.UnderOneYear })]
    [TestCase(new PetAge[] { PetAge.OneToThreeYears })]
    [TestCase(new PetAge[] { PetAge.ThreeToSevenYears })]
    [TestCase(new PetAge[] { PetAge.SevenToTenYears })]
    [TestCase(new PetAge[] { PetAge.OverTenYears })]
    [TestCase(new PetAge[] { PetAge.UnderOneYear, PetAge.OneToThreeYears, PetAge.ThreeToSevenYears, PetAge.SevenToTenYears, PetAge.OverTenYears })]
    public async Task WhenFilterByAge_ShouldReturnOnlyThatAge(PetAge[] ages)
    {
        // Arrange
        var pet1 = PetFixtures.FakePetEntity(1, age: PetAge.UnderOneYear);
        var pet2 = PetFixtures.FakePetEntity(2, age: PetAge.OneToThreeYears);
        var pet3 = PetFixtures.FakePetEntity(3, age: PetAge.ThreeToSevenYears);
        var pet4 = PetFixtures.FakePetEntity(4, age: PetAge.SevenToTenYears);
        var pet5 = PetFixtures.FakePetEntity(5, age: PetAge.OverTenYears);
        _dbContext.Pets.AddRange(pet1, pet2, pet3, pet4, pet5);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = PetFixtures.FakeGetPetsFilteredQuery(ages: ages.ToList());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(ages.Length);
        foreach (var item in result.Items)
            ages.Should().Contain(item.Age);
    }

    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 2)]
    [TestCase(1, 4)]
    [TestCase(2, 4)]
    public async Task WhenPaginate_ShouldPaginateCorrectly(int page, int pageSize)
    {
        // Arrange
        var pet1 = PetFixtures.FakePetEntity(1);
        var pet2 = PetFixtures.FakePetEntity(2);
        var pet3 = PetFixtures.FakePetEntity(3);
        _dbContext.Pets.AddRange(pet1, pet2, pet3);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = PetFixtures.FakeGetPetsFilteredQuery(page: page, pageSize: pageSize);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var pets = await _dbContext.Pets
            .AsNoTracking()
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToListAsync(CancellationToken.None);

        // Assert
        result.Items.Count.Should().Be(pets.Count);
    }
}
