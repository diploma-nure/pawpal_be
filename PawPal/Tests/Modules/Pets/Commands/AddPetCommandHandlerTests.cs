namespace Tests.Modules.Pets.Commands;

public class AddPetCommandHandlerTests : HandlerTestsBase
{
    private AddPetCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new AddPetCommandHandler(_dbContext, _mediaServiceMock.Object);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsAdmin_ShouldBeOk()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var command = PetFixtures.FakeAddPetCommand();

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

        var command = PetFixtures.FakeAddPetCommand();

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }
}