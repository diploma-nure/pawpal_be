namespace Application.Modules.Meetings.Commands;

public class ScheduleMeetingCommand(int applicationId) : IRequest<int>
{
    public int ApplicationId { get; set; } = applicationId;

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }
}
