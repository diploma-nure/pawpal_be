namespace Tests.Modules.Pets.Queries;

public class GetFollowupByIdQueryHanlderTests : HandlerTestsBase
{
    private const int ExistingPetId = 1;

    [SetUp]
    public void Setup()
    {
        var pet1 = PetFixtures.FakePetEntity(ExistingPetId);
        _dbContext.Pets.Add(pet1);
        _dbContext.SaveChanges();
    }

    [Test]
    public async Task WhenPet_Exists_ShouldBeOk()
    {
        //Arrange
        var query = new GetPetByIdQuery(ExistingPetId);
        var handler = new GetPetByIdQueryHandler(_dbContext);

        //Act
        var actualResult = await handler.Handle(query, CancellationToken.None);
        var expectedResult = await _dbContext.Pets
            .AsNoTracking()
            .Include(p => p.Features)
            .Include(p => p.Pictures)
            .FirstOrDefaultAsync(x => x.Id == ExistingPetId, CancellationToken.None);

        //Assert
        actualResult.Should().NotBeNull();
        expectedResult!.EqualTo(actualResult);
    }

    [Test]
    public async Task WhenPet_DoesNotExist_ShouldBeError()
    {
        //Arrange
        const int invalidId = 100;
        var query = new GetPetByIdQuery(invalidId);
        var handler = new GetPetByIdQueryHandler(_dbContext);

        //Act
        var act = async () => await handler.Handle(query, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet with id {query.PetId} not found");
    }
}