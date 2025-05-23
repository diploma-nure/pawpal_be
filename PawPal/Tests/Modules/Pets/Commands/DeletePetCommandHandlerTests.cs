namespace Tests.Modules.Pets.Commands;

public class DeletePetCommandHandlerTests : HandlerTestsBase
{
    private DeletePetCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new DeletePetCommandHandler(_dbContext);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsAdmin_ShouldDeletePet()
    {
        // Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = new DeletePetCommand(petId);

        // Act
        await _handler.Handle(command, CancellationToken.None);
        var deletedPet = _dbContext.Pets
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == pet.Id);

        // Assert
        deletedPet.Should().NotBeNull();
        deletedPet.DeletedAt.Should().NotBeNull();
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsNotAdmin_ShouldBeError()
    {
        // Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.User);
        _dbContext.User = user;

        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = new DeletePetCommand(petId);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }

    [Test]
    public async Task WhenPet_DoesNotExist_ShouldBeError()
    {
        // Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petId = 100;
        var command = new DeletePetCommand(petId);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet with id {petId} not found");
    }
}
