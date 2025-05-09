namespace Application.Modules.Meetings.Commands;

public class ChangeMeetingStatusCommand : IRequest<int>
{
    public int MeetingId { get; set; }

    public MeetingStatus? Status { get; set; }
}
