namespace Tests.Modules.Pets.Commands;

public class LikePetCommandHandlerTests : HandlerTestsBase
{
    private LikePetCommandHandler _likeHandler;

    private UnlikePetCommandHandler _unlikeHandler;

    [SetUp]
    public void SetUp()
    {
        _likeHandler = new LikePetCommandHandler(_dbContext);
        _unlikeHandler = new UnlikePetCommandHandler(_dbContext);
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

        var command = new LikePetCommand(petId);

        //Act
        await _likeHandler.Handle(command, CancellationToken.None);
        var model = _dbContext.PetLikes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId == userId && x.PetId == petId);

        //Assert
        model.Should().NotBeNull();
    }

    [Test]
    public async Task WhenInput_IsValid_AndPetAlreadyLiked_ShouldBeOk()
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

        var command = new LikePetCommand(petId);

        //Act
        await _likeHandler.Handle(command, CancellationToken.None);
        await _likeHandler.Handle(command, CancellationToken.None);
        var model = _dbContext.PetLikes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId == userId && x.PetId == petId);

        //Assert
        model.Should().NotBeNull();
    }

    [Test]
    public async Task WhenInput_IsValid_AndPetPreviouslyUnliked_ShouldBeLiked()
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

        var unlikeCommand = new UnlikePetCommand(petId);
        var likeCommand = new LikePetCommand(petId);

        //Act
        await _unlikeHandler.Handle(unlikeCommand, CancellationToken.None);
        await _likeHandler.Handle(likeCommand, CancellationToken.None);
        var model = _dbContext.PetLikes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId == userId && x.PetId == petId);

        //Assert
        model.Should().NotBeNull();
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
        var command = new LikePetCommand(petId);

        // Act
        var act = async () => await _likeHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet with id {petId} not found");
    }
}