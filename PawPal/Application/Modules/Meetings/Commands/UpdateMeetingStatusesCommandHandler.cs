namespace Application.Modules.Meetings.Commands;

public class UpdateMeetingStatusesCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateMeetingStatusesCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(UpdateMeetingStatusesCommand command, CancellationToken cancellationToken)
    {
        var currentDate = DateTime.UtcNow;
        var meetings = await _dbContext.Meetings
            .Where(m => m.Status == MeetingStatus.Scheduled && m.End < currentDate)
            .ToListAsync(cancellationToken);

        meetings.ForEach(m => m.Status = MeetingStatus.Completed);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return meetings.Count;
    }
}
