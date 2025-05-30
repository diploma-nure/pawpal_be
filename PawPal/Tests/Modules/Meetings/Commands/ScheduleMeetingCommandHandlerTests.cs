namespace Tests.Modules.Meetings.Commands;

[TestFixture]
public class ScheduleMeetingCommandHandlerTests : HandlerTestsBase
{
    private ScheduleMeetingCommandHandler _handler;

    private const int AdminId = 1;
    private const int UserId = 2;
    private const int PetId = 1;
    private const int ApplicationId = 1;

    [SetUp]
    public void SetUp()
    {
        var admin = UserFixtures.FakeUserEntity(AdminId, Role.Admin);
        _dbContext.Users.Add(admin);

        var user = UserFixtures.FakeUserEntity(UserId, Role.User);
        _dbContext.User = user;
        _dbContext.Users.Add(user);

        var pet = PetFixtures.FakePetEntity(PetId);
        _dbContext.Pets.Add(pet);

        var application = ApplicationFixtures.FakeApplicationEntity(ApplicationId, user.Id, pet.Id, status: ApplicationStatus.MeetingApproved);
        _dbContext.Applications.Add(application);

        _dbContext.SaveChanges();

        _handler = new ScheduleMeetingCommandHandler(_dbContext);
    }

    [Test]
    public async Task WhenSlot_IsAvailable_ShouldBeOk()
    {
        // Arrange
        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(10, 0));
        var end = date.ToDateWithTime(new TimeOnly(11, 0));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var id = await _handler.Handle(command, CancellationToken.None);
        var entity = _dbContext.Meetings
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);

        // Assert
        entity.Should().NotBeNull();
        entity.EqualTo(command);
        entity.AdminId.Should().Be(AdminId);
        entity.Status.Should().Be(MeetingStatus.Scheduled);
    }

    [Test]
    public void WhenUserRole_IsNotUser_ShouldBeError()
    {
        // Arrange
        var admin = _dbContext.Users.FirstOrDefault(x => x.Id == AdminId);
        _dbContext.User = admin;

        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(10, 0));
        var end = date.ToDateWithTime(new TimeOnly(11, 0));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }

    [Test]
    public async Task WhenApplication_DoesNotExist_ShouldBeError()
    {
        // Arrange
        var applicationId = 100;
        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(10, 0));
        var end = date.ToDateWithTime(new TimeOnly(11, 0));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(applicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Application with id {applicationId} not found");
    }

    [Test]
    public async Task WhenUser_IsNotOwnerOfApplication_ShouldBeError()
    {
        // Arrange
        var otherUser = UserFixtures.FakeUserEntity(99, Role.User);
        _dbContext.User = otherUser;
        _dbContext.Users.Add(otherUser);
        await _dbContext.SaveChangesAsync();

        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(10, 0));
        var end = date.ToDateWithTime(new TimeOnly(11, 0));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }

    [Test]
    public async Task WhenApplicationStatus_IsNotMeetingApprovedOrScheduled_ShouldBeError()
    {
        // Arrange
        var app = _dbContext.Applications.First(x => x.Id == ApplicationId);
        app.Status = ApplicationStatus.WaitingForConsideration;
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(10, 0));
        var end = date.ToDateWithTime(new TimeOnly(11, 0));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage($"Application must be in status {ApplicationStatus.MeetingApproved} or {ApplicationStatus.MeetingScheduled} to schedule a meeting");
    }

    [Test]
    public async Task WhenStartDate_IsInThePast_ShouldBeError()
    {
        // Arrange
        var start = DateTime.UtcNow.AddDays(-1).ToDateWithTime(new TimeOnly(10, 0));
        var end = start.AddHours(1);
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("Unable to schedule a meeting for this date");
    }

    [Test]
    public async Task WhenStartDate_IsOnWeekend_ShouldBeError()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var saturday = today.AddDays(((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7);
        var start = saturday.ToDateWithTime(new TimeOnly(10, 0));
        var end = saturday.ToDateWithTime(new TimeOnly(11, 0));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("Unable to schedule a meeting for this date");
    }

    [Test]
    public async Task WhenStartTime_IsBeforeWorkdayStart_ShouldBeError()
    {
        // Arrange
        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(6, 0));
        var end = date.ToDateWithTime(new TimeOnly(7, 30));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("Meeting is out of scheduled working time");
    }

    [Test]
    public async Task WhenEndTime_IsAfterWorkdayEnd_ShouldBeError()
    {
        // Arrange
        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(14, 30));
        var end = date.ToDateWithTime(new TimeOnly(17, 30));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("Meeting is out of scheduled working time");
    }

    [Test]
    public async Task WhenSlot_IsNotAvailable_ShouldBeError()
    {
        // Arrange
        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start = date.ToDateWithTime(new TimeOnly(10, 0));
        var end = date.ToDateWithTime(new TimeOnly(11, 0));

        var command1 = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start, end: end);

        var command2 = MeetingFixtures.FakeScheduleMeetingCommand(
            ApplicationId,
            start: start.AddMinutes(30),
            end: end.AddMinutes(30)
        );

        // Act
        await _handler.Handle(command1, CancellationToken.None);
        var act = () => _handler.Handle(command2, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("No available admins for the selected time slot");
    }

    [Test]
    public async Task WhenApplication_HasExistingFutureMeeting_ShouldBeOk()
    {
        // Arrange
        var date = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var start1 = date.ToDateWithTime(new TimeOnly(9, 0));
        var end1 = date.ToDateWithTime(new TimeOnly(10, 0));
        var command1 = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start1, end: end1);

        var start2 = date.ToDateWithTime(new TimeOnly(11, 0));
        var end2 = date.ToDateWithTime(new TimeOnly(12, 0));
        var command2 = MeetingFixtures.FakeScheduleMeetingCommand(ApplicationId, start: start2, end: end2);

        // Act
        var id1 = await _handler.Handle(command1, CancellationToken.None);
        var id2 = await _handler.Handle(command2, CancellationToken.None);

        // Assert: old gone, new there
        _dbContext.Meetings.AsNoTracking().FirstOrDefault(m => m.Id == id1)
                          .Should().BeNull();
        var newMeeting = _dbContext.Meetings.AsNoTracking().FirstOrDefault(m => m.Id == id2);
        newMeeting.Should().NotBeNull();
        newMeeting.Start.Should().Be(start2.ToNormalizedTime());
        newMeeting.End.Should().Be(end2.ToNormalizedTime());
    }

    [Test]
    public async Task WhenExistingMeeting_HasEnded_ShouldBeError()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.Date.AddDays(-1);
        var meeting = new Meeting
        {
            Id = 999,
            ApplicationId = ApplicationId,
            AdminId = AdminId,
            Start = pastDate.ToDateWithTime(new TimeOnly(10, 0)),
            End = pastDate.ToDateWithTime(new TimeOnly(11, 0)),
            Status = MeetingStatus.Scheduled
        };
        _dbContext.Meetings.Add(meeting);
        var app = _dbContext.Applications.First(x => x.Id == ApplicationId);
        app.Meeting = meeting;
        app.Status = ApplicationStatus.MeetingScheduled;
        await _dbContext.SaveChangesAsync();

        var futureDate = NextWeekday(DateTime.UtcNow.Date.AddDays(1));
        var command = MeetingFixtures.FakeScheduleMeetingCommand(
            ApplicationId,
            start: futureDate.ToDateWithTime(new TimeOnly(10, 0)),
            end: futureDate.ToDateWithTime(new TimeOnly(11, 0))
        );

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("Meeting for this application cannot be rescheduled");
    }

    private static DateTime NextWeekday(DateTime from)
    {
        var date = from;
        while (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            date = date.AddDays(1);
        return date;
    }
}
