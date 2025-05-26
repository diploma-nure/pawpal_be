namespace Tests.Modules.Meetings.Queries;

public class GetMeetingsQueryHandlerTests : HandlerTestsBase
{
    private GetMeetingsFilteredQueryHandler _handler;

    private const int AdminId = 1;
    private const int UserId = 2;
    private const int PetId = 1;

    [SetUp]
    public void SetUp()
    {
        _handler = new GetMeetingsFilteredQueryHandler(_dbContext);

        var admin = UserFixtures.FakeUserEntity(AdminId, Role.Admin);
        _dbContext.User = admin;
        _dbContext.Users.Add(admin);

        var user = UserFixtures.FakeUserEntity(UserId, Role.User);
        _dbContext.Users.Add(user);

        var pet = PetFixtures.FakePetEntity(PetId);
        _dbContext.Pets.Add(pet);

        _dbContext.SaveChanges();
    }

    [Test]
    public async Task WhenNoFilters_ShouldReturnAllMeetings_SortedByStartDesc()
    {
        // Arrange
        CreateApplicationAndMeeting(1, ApplicationStatus.MeetingScheduled, MeetingStatus.Scheduled);
        CreateApplicationAndMeeting(2, ApplicationStatus.MeetingScheduled, MeetingStatus.Scheduled);

        var query = MeetingFixtures.FakeGetMeetingsFilteredQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(2);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.Items.Select(x => x.Id).Should().Equal(2, 1);
    }

    [Test]
    [TestCase(new MeetingStatus[] { MeetingStatus.Scheduled })]
    [TestCase(new MeetingStatus[] { MeetingStatus.Cancelled })]
    [TestCase(new MeetingStatus[] { MeetingStatus.Completed })]
    [TestCase(new MeetingStatus[] { MeetingStatus.Scheduled, MeetingStatus.Cancelled, MeetingStatus.Completed })]
    public async Task WhenFilterByStatus_ShouldReturnOnlyThatStatus(MeetingStatus[] statuses)
    {
        // Arrange
        CreateApplicationAndMeeting(1, ApplicationStatus.MeetingScheduled, MeetingStatus.Scheduled);
        CreateApplicationAndMeeting(2, ApplicationStatus.MeetingScheduled, MeetingStatus.Cancelled);
        CreateApplicationAndMeeting(3, ApplicationStatus.MeetingScheduled, MeetingStatus.Completed);

        var query = MeetingFixtures.FakeGetMeetingsFilteredQuery(statuses: statuses.ToList());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(statuses.Length);
        foreach (var item in result.Items)
            statuses.Should().Contain(item.Status);
    }

    [Test]
    public async Task WhenInput_IsValid_AndUserIsNotAdmin_ShouldBeError()
    {
        // Arrange
        CreateApplicationAndMeeting(1, ApplicationStatus.MeetingScheduled, MeetingStatus.Scheduled);
        CreateApplicationAndMeeting(2, ApplicationStatus.MeetingScheduled, MeetingStatus.Scheduled);

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserId);
        _dbContext.User = user;

        var query = MeetingFixtures.FakeGetMeetingsFilteredQuery();

        //Act
        var act = async () => await _handler.Handle(query, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ForbiddenException>().WithMessage("Action forbidden");
    }

    private void CreateApplicationAndMeeting(int id, ApplicationStatus applicationStatus, MeetingStatus meetingStatus)
    {
        var application = ApplicationFixtures.FakeApplicationEntity(id, UserId, PetId, status: applicationStatus);
        _dbContext.Applications.Add(application);

        var currentDate = DateTime.UtcNow;
        var meeting = MeetingFixtures.FakeMeetingEntity(id, AdminId, id, status: meetingStatus, start: currentDate.AddDays(id), end: currentDate.AddDays(id).AddHours(1));
        _dbContext.Meetings.Add(meeting);

        _dbContext.SaveChanges();
    }
}
