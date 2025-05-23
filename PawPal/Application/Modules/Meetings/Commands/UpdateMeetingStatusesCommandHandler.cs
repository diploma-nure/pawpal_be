namespace Application.Modules.Meetings.Commands;

public class UpdateMeetingStatusesCommandHandler(IApplicationDbContext dbContext, IMeetingService meetingService)
    : IRequestHandler<UpdateMeetingStatusesCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly IMeetingService _meetingService = meetingService;

    public async Task<int> Handle(UpdateMeetingStatusesCommand command, CancellationToken cancellationToken)
    {
        var currentDate = DateTime.UtcNow;
        var meetings = await _dbContext.Meetings
            .Where(m => m.Status == MeetingStatus.Scheduled && m.End < currentDate)
            .ToListAsync(cancellationToken);

        foreach (var meeting in meetings)
        {
            meeting.Status = MeetingStatus.Completed;
            await _meetingService.DeleteRoomAsync(meeting.Id);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return meetings.Count;
    }
}
