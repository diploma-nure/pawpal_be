namespace Tests.Common;

[TestFixture]
public abstract class HandlerTestsBase
{
    protected ApplicationDbContext _dbContext;

    protected Mock<IEmailService> _emailServiceMock;

    protected Mock<IMediaService> _mediaServiceMock;

    protected Mock<IMeetingService> _meetingServiceMock;

    protected Mock<IMediator> _mediatorMock;

    [SetUp]
    public virtual void OneTimeSetUp()
    {
        var dbName = Guid.NewGuid().ToString();
        var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        _dbContext = new(dbOptions);

        _emailServiceMock = new Mock<IEmailService>();
        _mediaServiceMock = new Mock<IMediaService>();
        _meetingServiceMock = new Mock<IMeetingService>();
        _mediatorMock = new Mock<IMediator>();
    }
}
