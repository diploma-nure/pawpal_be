namespace Tests.Modules.Applications.Commands;

public class SubmitApplicationCommandHandlerTests : HandlerTestsBase
{
    private SubmitApplicationCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new SubmitApplicationCommandHandler(_dbContext);
    }

    [Test]
    public async Task WhenInput_IsValid_ShouldBeOk()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);

        var pet = PetFixtures.FakePetEntity(1);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = ApplicationFixtures.FakeSubmitApplicationCommand(pet.Id);

        //Act
        var id = await _handler.Handle(command, CancellationToken.None);
        var entity = _dbContext.Applications
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        //Assert
        entity.Should().NotBeNull();
        entity!.EqualTo(command);
        entity.UserId.Should().Be(user.Id);
        entity.Status.Should().Be(ApplicationStatus.WaitingForConsideration);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsAdmin_ShouldBeError()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.Admin);
        _dbContext.User = user;

        var petId = 1;
        var command = ApplicationFixtures.FakeSubmitApplicationCommand(petId);

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }

    [Test]
    public async Task WhenPet_DoesNotExist_ShouldBeError()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.User);
        _dbContext.User = user;

        var petId = 100;
        var command = ApplicationFixtures.FakeSubmitApplicationCommand(petId);

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Pet with id {petId} not found");
    }

    [Test]
    public async Task WhenInput_IsValid_AndActiveApplicationExists_ShouldBeOk()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);

        var pet = PetFixtures.FakePetEntity(1);
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = ApplicationFixtures.FakeSubmitApplicationCommand(pet.Id);

        //Act
        await _handler.Handle(command, CancellationToken.None);
        await _handler.Handle(command, CancellationToken.None);
        var entities = await _dbContext.Applications
            .AsNoTracking()
            .Where(x => x.PetId == pet.Id && x.UserId == user.Id)
            .ToListAsync(CancellationToken.None);

        //Assert
        entities.Should().NotBeNull();
        entities.Count.Should().Be(1);
    }

    [Test]
    public async Task WhenInput_IsValid_AndRejectedApplicationExists_ShouldCreateNew()
    {
        //Arrange
        var user = UserFixtures.FakeUserEntity(1, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);

        var pet = PetFixtures.FakePetEntity(1);
        _dbContext.Pets.Add(pet);

        var rejectedApplication = ApplicationFixtures.FakeApplicationEntity(1, user.Id, pet.Id, status: ApplicationStatus.Rejected);
        _dbContext.Applications.Add(rejectedApplication);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = ApplicationFixtures.FakeSubmitApplicationCommand(pet.Id);

        //Act
        await _handler.Handle(command, CancellationToken.None);
        var entities = await _dbContext.Applications
            .AsNoTracking()
            .Where(x => x.PetId == pet.Id && x.UserId == user.Id)
            .ToListAsync(CancellationToken.None);

        //Assert
        entities.Should().NotBeNull();
        entities.Count.Should().Be(2);
        entities.Should().ContainSingle(x => x.Status == ApplicationStatus.WaitingForConsideration);
        entities.Should().ContainSingle(x => x.Status == ApplicationStatus.Rejected);
    }
}