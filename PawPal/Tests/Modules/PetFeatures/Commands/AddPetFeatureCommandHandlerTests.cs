namespace Tests.Modules.PetFeatures.Commands;

public class AddPetFeatureCommandHandlerTests : HandlerTestsBase
{
    private AddPetFeatureCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new AddPetFeatureCommandHandler(_dbContext);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsAdmin_ShouldBeOk()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var command = PetFeatureFixtures.FakeAddPetFeatureCommand();

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

        var command = PetFeatureFixtures.FakeAddPetFeatureCommand();

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }
}