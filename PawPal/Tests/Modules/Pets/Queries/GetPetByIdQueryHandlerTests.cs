namespace Tests.Modules.Pets.Queries;

public class GetPetByIdQueryHanlderTests : HandlerTestsBase
{
    private GetPetByIdQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new GetPetByIdQueryHandler(_dbContext);
    }

    [Test]
    public async Task WhenPet_Exists_ShouldBeOk()
    {
        //Arrange
        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = new GetPetByIdQuery(petId);

        //Act
        var actualResult = await _handler.Handle(query, CancellationToken.None);
        var expectedResult = await _dbContext.Pets
            .AsNoTracking()
            .Include(p => p.Features)
            .Include(p => p.Pictures)
            .FirstOrDefaultAsync(x => x.Id == petId, CancellationToken.None);

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

        //Act
        var act = async () => await _handler.Handle(query, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet with id {query.PetId} not found");
    }
}