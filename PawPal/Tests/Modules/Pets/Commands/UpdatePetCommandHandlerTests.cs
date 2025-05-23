namespace Tests.Modules.Pets.Commands;

public class UpdatePetCommandHandlerTests : HandlerTestsBase
{
    private UpdatePetCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new UpdatePetCommandHandler(_dbContext, _mediaServiceMock.Object);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsAdmin_ShouldBeOk()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = PetFixtures.FakeUpdatePetCommand(petId);

        //Act
        var id = await _handler.Handle(command, CancellationToken.None);
        var model = _dbContext.Pets
            .AsNoTracking()
            .Include(p => p.Features)
            .FirstOrDefault(x => x.Id == id);

        //Assert
        model.Should().NotBeNull();
        model!.EqualTo(command);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsNotAdmin_ShouldBeError()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.User);
        _dbContext.User = user;

        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = PetFixtures.FakeUpdatePetCommand(petId);

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }

    [Test]
    public async Task WhenPet_DoesNotExist_ShouldBeError()
    {
        // Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petId = 100;
        var command = PetFixtures.FakeUpdatePetCommand(petId);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet with id {petId} not found");
    }
}