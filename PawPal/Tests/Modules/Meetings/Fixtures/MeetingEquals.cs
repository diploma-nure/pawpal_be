namespace Tests.Modules.Meetings.Fixtures;

public static class MeetingEquals
{
    public static void EqualTo(this Meeting entity, ScheduleMeetingCommand command)
    {
        entity.ApplicationId.Should().Be(command.ApplicationId);
        entity.Start.Should().Be(command.Start);
        entity.End.Should().Be(command.End);
    }

    public static void EqualTo(this Meeting entity, ChangeMeetingStatusCommand command)
    {
        entity.Id.Should().Be(command.MeetingId);
        entity.Status.Should().Be(command.Status);
    }
}
