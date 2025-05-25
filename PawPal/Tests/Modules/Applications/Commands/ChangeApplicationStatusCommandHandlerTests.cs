namespace Tests.Modules.Applications.Commands;

public class ChangeApplicationStatusCommandHandlerTests : HandlerTestsBase
{
    private ChangeApplicationStatusCommandHandler _handler;

    private const int AdminId = 1;
    private const int UserId = 2;
    private const int PetId = 1;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePetCommand>(), It.IsAny<CancellationToken>()))
            .Returns<DeletePetCommand, CancellationToken>(
                (x, t) => new DeletePetCommandHandler(_dbContext).Handle(x, t));

        _handler = new ChangeApplicationStatusCommandHandler(_dbContext, _mediatorMock.Object);

        var user = UserFixtures.FakeUserEntity(UserId, Role.User);
        _dbContext.Users.Add(user);

        var admin = UserFixtures.FakeUserEntity(AdminId, Role.Admin);
        _dbContext.Users.Add(admin);
        _dbContext.User = admin;

        var pet = PetFixtures.FakePetEntity(PetId);
        _dbContext.Pets.Add(pet);
        _dbContext.SaveChanges();
    }

    [Test]
    public async Task WhenInput_IsValid_ShouldBeOk()
    {
        //Arrange
        var application = ApplicationFixtures.FakeApplicationEntity(1, UserId, PetId, status: ApplicationStatus.WaitingForConsideration);
        _dbContext.Applications.Add(application);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = ApplicationFixtures.FakeChangeApplicationStatusCommand(application.Id, ApplicationStatus.MeetingApproved);

        //Act
        var id = await _handler.Handle(command, CancellationToken.None);
        var entity = _dbContext.Applications
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        //Assert
        entity.Should().NotBeNull();
        entity!.EqualTo(command);
    }

    [Test]
    public async Task WhenStatus_ChangedToRejected_ShouldCancelMeeting()
    {
        //Arrange
        var application = ApplicationFixtures.FakeApplicationEntity(1, UserId, PetId, status: ApplicationStatus.MeetingScheduled);
        _dbContext.Applications.Add(application);

        var currentDate = DateTime.UtcNow;
        var meeting = MeetingFixtures.FakeMeetingEntity(1, AdminId, application.Id, currentDate.AddHours(-1), currentDate, status: MeetingStatus.Scheduled);
        _dbContext.Meetings.Add(meeting);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = ApplicationFixtures.FakeChangeApplicationStatusCommand(application.Id, ApplicationStatus.Rejected);

        //Act
        var id = await _handler.Handle(command, CancellationToken.None);
        var entity = _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.Meeting)
            .FirstOrDefault(x => x.Id == id);

        //Assert
        entity.Should().NotBeNull();
        entity!.EqualTo(command);
        entity.Meeting.Should().NotBeNull();
        entity.Meeting.Status.Should().Be(MeetingStatus.Cancelled);
    }

    [Test]
    public async Task WhenStatus_ChangedToApproved_ShouldCancelMeetings_AndRejectApplications()
    {
        //Arrange
        var currentDate = DateTime.UtcNow;
        var applicationId = 2;
        for (var id = applicationId; id <= 5; id++)
        {
            var application = ApplicationFixtures.FakeApplicationEntity(id, UserId, PetId, status: ApplicationStatus.WaitingForConsideration);
            _dbContext.Applications.Add(application);

            var meeting = MeetingFixtures.FakeMeetingEntity(id, AdminId, application.Id, currentDate.AddHours(-id), currentDate, status: MeetingStatus.Scheduled);
            _dbContext.Meetings.Add(meeting);
        }

        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = ApplicationFixtures.FakeChangeApplicationStatusCommand(applicationId, ApplicationStatus.Approved);

        //Act
        await _handler.Handle(command, CancellationToken.None);
        var entities = await _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.Meeting)
            .Where(x => x.PetId == PetId)
            .ToListAsync(CancellationToken.None);

        //Assert
        var approvedApplication = entities.FirstOrDefault(x => x.Id == applicationId);
        approvedApplication.Should().NotBeNull();
        approvedApplication!.Status.Should().Be(ApplicationStatus.Approved);
        approvedApplication.Meeting.Should().NotBeNull();
        approvedApplication.Meeting.Status.Should().Be(MeetingStatus.Cancelled);

        var rejectedApplications = entities.Where(x => x.Id != applicationId).ToList();
        foreach (var application in rejectedApplications)
        {
            application.Status.Should().Be(ApplicationStatus.Rejected);
            application.Meeting.Should().NotBeNull();
            application.Meeting.Status.Should().Be(MeetingStatus.Cancelled);
        }
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsNotAdmin_ShouldBeError()
    {
        //Arrange
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserId);
        _dbContext.User = user;

        var application = ApplicationFixtures.FakeApplicationEntity(1, UserId, PetId, status: ApplicationStatus.WaitingForConsideration);
        _dbContext.Applications.Add(application);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var command = ApplicationFixtures.FakeChangeApplicationStatusCommand(application.Id, ApplicationStatus.MeetingApproved);

        //Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }
}