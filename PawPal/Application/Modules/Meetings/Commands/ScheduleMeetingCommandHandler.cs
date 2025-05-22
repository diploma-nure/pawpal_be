namespace Application.Modules.Meetings.Commands;

public class ScheduleMeetingCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ScheduleMeetingCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(ScheduleMeetingCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.User)
            throw new ForbiddenException();

        var application = await _dbContext.Applications
            .Include(a => a.Meeting)
            .FirstOrDefaultAsync(a => a.Id == command.ApplicationId, cancellationToken)
            ?? throw new NotFoundException($"Application with id {command.ApplicationId} not found");

        if (application.UserId != _dbContext.User.Id)
            throw new ForbiddenException("You are not allowed to schedule a meeting for this application");

        if (application.Status is not ApplicationStatus.MeetingApproved && application.Status is not ApplicationStatus.MeetingScheduled)
            throw new ConflictException($"Application must be in status {ApplicationStatus.MeetingApproved} or {ApplicationStatus.MeetingScheduled} to schedule a meeting");

        var currentDate = DateTime.UtcNow;
        if (application.Meeting is not null)
        {
            if (application.Meeting.End < currentDate)
                throw new ConflictException("Meeting for this application cannot be rescheduled");

            _dbContext.Meetings.Remove(application.Meeting);
        }

        var workDayStartTime = new TimeOnly(7, 0);
        var workDayEndTime = new TimeOnly(16, 0);

        var start = command.Start!.Value.ToNormalizedTime();
        var end = command.End!.Value.ToNormalizedTime();

        if (start.TimeOfDay < workDayStartTime.ToTimeSpan() || end.TimeOfDay > workDayEndTime.ToTimeSpan())
            throw new ConflictException("Meeting is out of scheduled working time");

        if (start.Date < currentDate.Date || (start.Date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday))
            throw new ConflictException("Unable to schedule a meeting for this date");

        if (start.Date == currentDate.Date && start.TimeOfDay < currentDate.TimeOfDay)
            throw new ConflictException("Unable to schedule a meeting for this time");

        var availableAdmin = await _dbContext.Users
            .Where(a => a.Role == Role.Admin && a.Meetings.All(m => m.Status == MeetingStatus.Cancelled || (!((start >= m.Start && start < m.End) || (end <= m.End && end > m.Start)))))
            .OrderBy(a => a.Meetings.Where(m => m.Start > currentDate).Count())
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ConflictException("No available admins for the selected time slot");

        var meeting = new Meeting
        {
            Start = start,
            End = end,
            ApplicationId = application.Id,
            AdminId = availableAdmin.Id,
            Status = MeetingStatus.Scheduled
        };

        _dbContext.Meetings.Add(meeting);
        application.Status = ApplicationStatus.MeetingScheduled;
        await _dbContext.SaveChangesAsync(cancellationToken);

        var testApplication = await _dbContext.Applications.Include(a => a.Meeting).FirstOrDefaultAsync(a => a.Id == command.ApplicationId, cancellationToken);

        return meeting.Id;
    }
}
