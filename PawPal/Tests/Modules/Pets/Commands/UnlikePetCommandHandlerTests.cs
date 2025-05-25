namespace Tests.Modules.Pets.Commands;

public class UnlikePetCommandHandlerTests : HandlerTestsBase
{
    private UnlikePetCommandHandler _unlikeHandler;

    private LikePetCommandHandler _likeHandler;

    [SetUp]
    public void SetUp()
    {
        _unlikeHandler = new UnlikePetCommandHandler(_dbContext);
        _likeHandler = new LikePetCommandHandler(_dbContext);
    }

    [Test]
    public async Task WhenInput_IsValid_ShouldBeOk()
    {
        //Arrange
        var userId = 1;
        var user = UserFixtures.FakeUserEntity(userId, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);

        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = new UnlikePetCommand(petId);

        //Act
        await _unlikeHandler.Handle(command, CancellationToken.None);
        var entity = _dbContext.PetLikes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId == userId && x.PetId == petId);

        //Assert
        entity.Should().BeNull();
    }

    [Test]
    public async Task WhenInput_IsValid_AndPetAlreadyUnliked_ShouldBeOk()
    {
        //Arrange
        var userId = 1;
        var user = UserFixtures.FakeUserEntity(userId, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);

        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = new UnlikePetCommand(petId);

        //Act
        await _unlikeHandler.Handle(command, CancellationToken.None);
        await _unlikeHandler.Handle(command, CancellationToken.None);
        var entity = _dbContext.PetLikes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId == userId && x.PetId == petId);

        //Assert
        entity.Should().BeNull();
    }

    [Test]
    public async Task WhenInput_IsValid_AndPetPreviouslyLiked_ShouldBeUnliked()
    {
        //Arrange
        var userId = 1;
        var user = UserFixtures.FakeUserEntity(userId, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);

        var petId = 1;
        var pet = PetFixtures.FakePetEntity(petId);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var likeCommand = new LikePetCommand(petId);
        var unlikeCommand = new UnlikePetCommand(petId);

        //Act
        await _likeHandler.Handle(likeCommand, CancellationToken.None);
        await _unlikeHandler.Handle(unlikeCommand, CancellationToken.None);
        var entity = _dbContext.PetLikes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId == userId && x.PetId == petId);

        //Assert
        entity.Should().BeNull();
    }

    [Test]
    public async Task WhenPet_DoesNotExist_ShouldBeError()
    {
        // Arrange
        var userId = 1;
        var user = UserFixtures.FakeUserEntity(userId, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var petId = 100;
        var command = new UnlikePetCommand(petId);

        // Act
        var act = async () => await _unlikeHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet with id {petId} not found");
    }
}