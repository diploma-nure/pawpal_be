namespace Tests.Modules.PetFeatures.Commands;

public class UpdatePetFeatureCommandHandlerTests : HandlerTestsBase
{
    private UpdatePetFeatureCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new UpdatePetFeatureCommandHandler(_dbContext);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsAdmin_ShouldBeOk()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petFeature = PetFeatureFixtures.FakePetFeatureEntity(1);
        _dbContext.PetFeatures.Add(petFeature);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = PetFeatureFixtures.FakeUpdatePetFeatureCommand(petFeature.Id);

        //Act
        var id = await _handler.Handle(command, CancellationToken.None);
        var entity = _dbContext.PetFeatures
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        //Assert
        entity.Should().NotBeNull();
        entity!.EqualTo(command);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsNotAdmin_ShouldBeError()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.User);
        _dbContext.User = user;

        var petFeature = PetFeatureFixtures.FakePetFeatureEntity(1);
        _dbContext.PetFeatures.Add(petFeature);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = PetFeatureFixtures.FakeUpdatePetFeatureCommand(petFeature.Id);

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
        var command = PetFeatureFixtures.FakeUpdatePetFeatureCommand(petId);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet Feature with id {petId} not found");
    }
}