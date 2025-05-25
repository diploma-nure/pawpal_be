namespace Tests.Modules.Applications.Queries;

public class GetApplicationsFilteredQueryHandlerTests : HandlerTestsBase
{
    private GetApplicationsFilteredQueryHandler _handler;

    private const int UserId = 2;
    private const int PetId = 1;

    [SetUp]
    public void SetUp()
    {
        _handler = new GetApplicationsFilteredQueryHandler(_dbContext);

        var user = UserFixtures.FakeUserEntity(UserId, Role.User);
        _dbContext.Users.Add(user);
        _dbContext.User = user;

        var pet = PetFixtures.FakePetEntity(PetId);
        _dbContext.Pets.Add(pet);
        _dbContext.SaveChanges();
    }

    [Test]
    public async Task WhenNoFilters_ShouldReturnAllApplications_SortedByUpdatedAtDesc()
    {
        // Arrange
        var currentDate = DateTime.UtcNow;
        var application1 = ApplicationFixtures.FakeApplicationEntity(1, UserId, PetId, updatedAt: currentDate);
        var application2 = ApplicationFixtures.FakeApplicationEntity(2, UserId, PetId, updatedAt: currentDate.AddDays(-1));
        _dbContext.Applications.AddRange(application1, application2);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = ApplicationFixtures.FakeGetApplicationsFilteredQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(2);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.Items.Select(x => x.Id).Should().Equal(application1.Id, application2.Id);
    }

    [Test]
    [TestCase(new ApplicationStatus[] { ApplicationStatus.WaitingForConsideration })]
    [TestCase(new ApplicationStatus[] { ApplicationStatus.MeetingApproved })]
    [TestCase(new ApplicationStatus[] { ApplicationStatus.MeetingScheduled })]
    [TestCase(new ApplicationStatus[] { ApplicationStatus.WaitingForDecision })]
    [TestCase(new ApplicationStatus[] { ApplicationStatus.Rejected })]
    [TestCase(new ApplicationStatus[] { ApplicationStatus.Approved })]
    [TestCase(new ApplicationStatus[] { ApplicationStatus.WaitingForConsideration, ApplicationStatus.MeetingApproved, ApplicationStatus.MeetingScheduled, ApplicationStatus.WaitingForDecision, ApplicationStatus.Rejected, ApplicationStatus.Approved })]
    public async Task WhenFilterByStatus_ShouldReturnOnlyThatStatus(ApplicationStatus[] statuses)
    {
        // Arrange
        var application1 = ApplicationFixtures.FakeApplicationEntity(1, UserId, PetId, status: ApplicationStatus.WaitingForConsideration);
        var application2 = ApplicationFixtures.FakeApplicationEntity(2, UserId, PetId, status: ApplicationStatus.MeetingApproved);
        var application3 = ApplicationFixtures.FakeApplicationEntity(3, UserId, PetId, status: ApplicationStatus.MeetingScheduled);
        var application4 = ApplicationFixtures.FakeApplicationEntity(4, UserId, PetId, status: ApplicationStatus.WaitingForDecision);
        var application5 = ApplicationFixtures.FakeApplicationEntity(5, UserId, PetId, status: ApplicationStatus.Rejected);
        var application6 = ApplicationFixtures.FakeApplicationEntity(6, UserId, PetId, status: ApplicationStatus.Approved);
        _dbContext.Applications.AddRange(application1, application2, application3, application4, application5, application6);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var query = ApplicationFixtures.FakeGetApplicationsFilteredQuery(statuses: statuses.ToList());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Count.Should().Be(statuses.Length);
        foreach (var item in result.Items)
            statuses.Should().Contain(item.Status);
    }
}
