namespace Tests.Modules.Pets.Commands;

public class AddPetCommandHandlerTests : HandlerTestsBase
{
    [SetUp]
    public void Setup()
    {
        var user = new User() { Role = Role.Admin };
        _dbContext.User = user;
    }

    [Test]
    public async Task WhenInput_IsValid_ShouldBeOk()
    {
        //Arrange
        var command = PetFixtures.FakeAddPetCommand();
        var handler = new AddPetCommandHandler(_dbContext, _mediaServiceMock.Object);

        //Act
        var id = await handler.Handle(command, CancellationToken.None);
        var model = _dbContext.Pets
            .AsNoTracking()
            .Include(p => p.Features)
            .FirstOrDefault(x => x.Id == id);

        //Assert
        model.Should().NotBeNull();
        model!.EqualTo(command);
    }
}