namespace Tests.Modules.PetFeatures.Commands;

public class DeletePetFeatureCommandHandlerTests : HandlerTestsBase
{
    private DeletePetFeatureCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new DeletePetFeatureCommandHandler(_dbContext);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsAdmin_ShouldDeletePetFeature()
    {
        // Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petFeature = PetFeatureFixtures.FakePetFeatureEntity(1);
        _dbContext.PetFeatures.Add(petFeature);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = new DeletePetFeatureCommand(petFeature.Id);

        // Act
        await _handler.Handle(command, CancellationToken.None);
        var deletedPet = _dbContext.PetFeatures
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == petFeature.Id);

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

        var petFeature = PetFeatureFixtures.FakePetFeatureEntity(1);
        _dbContext.PetFeatures.Add(petFeature);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = new DeletePetFeatureCommand(petFeature.Id);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }

    [Test]
    public async Task WhenPetFeature_DoesNotExist_ShouldBeError()
    {
        // Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petFeatureId = 100;
        var command = new DeletePetFeatureCommand(petFeatureId);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet Feature with id {petFeatureId} not found");
    }
}
