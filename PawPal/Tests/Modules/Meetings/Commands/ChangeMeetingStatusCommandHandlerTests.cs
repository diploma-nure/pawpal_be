namespace Tests.Modules.Meetings.Commands;

public class ChangeMeetingStatusCommandHandlerTests : HandlerTestsBase
{
    private ChangeMeetingStatusCommandHandler _handler;

    private const int AdminId = 1;
    private const int UserId = 2;
    private const int PetId = 1;
    private const int ApplicationId = 1;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePetCommand>(), It.IsAny<CancellationToken>()))
            .Returns<DeletePetCommand, CancellationToken>(
                (x, t) => new DeletePetCommandHandler(_dbContext).Handle(x, t));

        _handler = new ChangeMeetingStatusCommandHandler(_dbContext);

        var user = UserFixtures.FakeUserEntity(UserId, Role.User);
        _dbContext.Users.Add(user);

        var admin = UserFixtures.FakeUserEntity(AdminId, Role.Admin);
        _dbContext.Users.Add(admin);
        _dbContext.User = admin;

        var pet = PetFixtures.FakePetEntity(PetId);
        _dbContext.Pets.Add(pet);

        var application = ApplicationFixtures.FakeApplicationEntity(ApplicationId, user.Id, pet.Id, status: ApplicationStatus.MeetingApproved);
        _dbContext.Applications.Add(application);

        _dbContext.SaveChanges();
    }

    [Test]
    public async Task WhenInput_IsValid_ShouldBeOk()
    {
        //Arrange
        var currentDate = DateTime.UtcNow;
        var meeting = MeetingFixtures.FakeMeetingEntity(1, AdminId, ApplicationId, start: currentDate, end: currentDate.AddHours(1));
        _dbContext.Meetings.Add(meeting);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = MeetingFixtures.FakeChangeMeetingStatusCommand(meeting.Id, MeetingStatus.Completed);

        //Act
        var id = await _handler.Handle(command, CancellationToken.None);
        var entity = _dbContext.Meetings
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        //Assert
        entity.Should().NotBeNull();
        entity!.EqualTo(command);
    }

    [Test]
    [TestCase(MeetingStatus.Cancelled)]
    [TestCase(MeetingStatus.Completed)]
    public async Task WhenMeeting_AlreadyCompletedOrCancelled_ShouldBeError(MeetingStatus status)
    {
        //Arrange
        var currentDate = DateTime.UtcNow;
        var meeting = MeetingFixtures.FakeMeetingEntity(1, AdminId, ApplicationId, start: currentDate, end: currentDate.AddHours(1), status: status);
        _dbContext.Meetings.Add(meeting);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = MeetingFixtures.FakeChangeMeetingStatusCommand(meeting.Id, MeetingStatus.Completed);

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("Meeting is already cancelled or completed");
    }

    [Test]
    public async Task WhenMeeting_DoesNotExist_ShouldBeError()
    {
        //Arrange
        var meetingId = 100;
        var command = MeetingFixtures.FakeChangeMeetingStatusCommand(meetingId, MeetingStatus.Completed);

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Meeting with id {meetingId} not found");
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsNotAdmin_ShouldBeError()
    {
        //Arrange
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserId);
        _dbContext.User = user;

        var currentDate = DateTime.UtcNow;
        var meeting = MeetingFixtures.FakeMeetingEntity(1, AdminId, ApplicationId, start: currentDate, end: currentDate.AddHours(1));
        _dbContext.Meetings.Add(meeting);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = MeetingFixtures.FakeChangeMeetingStatusCommand(meeting.Id, MeetingStatus.Completed);

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }
}