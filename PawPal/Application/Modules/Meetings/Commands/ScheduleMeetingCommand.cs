namespace Application.Modules.Meetings.Commands;

public class ScheduleMeetingCommand : IRequest<int>
{
    public int ApplicationId { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }
}
