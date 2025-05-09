namespace Application.Modules.Meetings.Commands;

public class JoinMeetingCommand : IRequest<MeetingJoinInfoDto>
{
    public int? ApplicationId { get; set; }

    public int? MeetingId { get; set; }
}
