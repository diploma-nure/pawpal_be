namespace Tests.Modules.Meetings.Fixtures;

public static class MeetingFixtures
{
    public static Meeting FakeMeetingEntity(
        int id,
        int adminId,
        int applicationId,
        DateTime start,
        DateTime end,
        MeetingStatus? status = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
    {
        var faker = new Faker<Meeting>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => id)
            .RuleFor(x => x.Start, f => start)
            .RuleFor(x => x.End, (f, m) => end)
            .RuleFor(x => x.Status, f => status ?? f.PickRandom<MeetingStatus>())
            .RuleFor(x => x.AdminId, f => adminId)
            .RuleFor(x => x.Admin, f => null!)
            .RuleFor(x => x.ApplicationId, f => applicationId)
            .RuleFor(x => x.Application, f => null!)
            .RuleFor(x => x.CreatedAt, f => createdAt ?? DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, f => updatedAt ?? DateTime.UtcNow);

        faker.Validate();

        return faker.Generate();
    }

    public static ScheduleMeetingCommand FakeScheduleMeetingCommand(
        int applicationId,
        DateTime? start = null,
        DateTime? end = null)
    {
        var faker = new Faker<ScheduleMeetingCommand>()
            .StrictMode(true)
            .RuleFor(x => x.ApplicationId, f => applicationId)
            .RuleFor(x => x.Start, f => start ?? f.Date.FutureOffset().UtcDateTime)
            .RuleFor(x => x.End, (f, m) => end ?? (start ?? DateTime.UtcNow).AddHours(1));

        faker.Validate();

        return faker.Generate();
    }

    public static ChangeMeetingStatusCommand FakeChangeMeetingStatusCommand(
        int meetingId,
        MeetingStatus status)
    {
        var faker = new Faker<ChangeMeetingStatusCommand>()
            .StrictMode(true)
            .RuleFor(x => x.MeetingId, f => meetingId)
            .RuleFor(x => x.Status, status);

        faker.Validate();

        return faker.Generate();
    }
}
