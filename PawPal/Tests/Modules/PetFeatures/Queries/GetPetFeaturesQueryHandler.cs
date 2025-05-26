namespace Tests.Modules.PetFeatures.Queries;

public class GetPetFeaturesQueryHandlerTests : HandlerTestsBase
{
    private GetPetFeaturesQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new GetPetFeaturesQueryHandler(_dbContext);
    }

    [Test]
    public async Task WhenGet_ShouldReturnAllPetFeatures_SortedByNameAsc()
    {
        // Arrange
        var pet1 = PetFeatureFixtures.FakePetFeatureEntity(1, feature: "aaa");
        var pet2 = PetFeatureFixtures.FakePetFeatureEntity(2, feature: "bbb");
        _dbContext.PetFeatures.AddRange(pet1, pet2);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = new GetPetFeaturesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(2);
        result.Select(x => x.Feature).Should().Equal(pet1.Feature, pet2.Feature);
    }

    [Test]
    public async Task WhenGet_ShouldReturnAllPetFeatures_WithoutSoftDeleted()
    {
        // Arrange
        var pet1 = PetFeatureFixtures.FakePetFeatureEntity(1, feature: "aaa");
        var pet2 = PetFeatureFixtures.FakePetFeatureEntity(2, feature: "bbb", deletedAt: DateTime.UtcNow);
        _dbContext.PetFeatures.AddRange(pet1, pet2);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = new GetPetFeaturesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(1);
        result.Select(x => x.Id).Should().Equal(pet1.Id);
    }
}
