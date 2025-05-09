namespace Application.Modules.Meetings.Queries;

public class GetMeetingSlotsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetMeetingSlotsQuery, List<DaySlotDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<List<DaySlotDto>> Handle(GetMeetingSlotsQuery query, CancellationToken cancellationToken)
    {
        var globalStartDate = query.StartDate!.Value.ToNormalizedTime();
        var globalEndDate = query.EndDate!.Value.ToNormalizedTime();
        var currentDate = DateTime.UtcNow;

        var result = new List<DaySlotDto>();
        var workDayStartTime = new TimeOnly(7, 0);
        var workDayEndTime = new TimeOnly(16, 0);

        var admins = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Role == Role.Admin)
                .ToListAsync(cancellationToken);

        foreach (var admin in admins)
        {
            var existingMeetings = await _dbContext.Meetings
                .AsNoTracking()
                .Where(m => m.Start >= globalStartDate && m.End <= globalEndDate)
                .ToListAsync(cancellationToken);

            admin.Meetings = existingMeetings;
        }

        for (var date = globalStartDate; date < globalEndDate; date = date.AddDays(1))
        {
            var daySlot = new DaySlotDto()
            {
                Date = DateOnly.FromDateTime(date),
                IsAvailable = false,
                TimeSlots = []
            };

            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                result.Add(daySlot);
                continue;
            }

            for (var time = workDayStartTime; time < workDayEndTime; time = time.AddHours(1))
            {
                var isAvailable = false;

                var startDate = date.ToDateWithTime(time);
                var endDate = startDate.AddHours(1);

                if (date.Date < currentDate.Date)
                    continue;
                else if (date.Date == currentDate.Date && time.ToTimeSpan() < currentDate.TimeOfDay)
                    isAvailable = false;
                else if (admins.Any(a => a.Meetings.All(m => !DateTimeHelper.AreTimesConflicting(startDate, endDate, m.Start, m.End))))
                    isAvailable = true;

                daySlot.TimeSlots.Add(new TimeSlotDto() { Time = time, IsAvailable = isAvailable });
            }

            daySlot.IsAvailable = daySlot.TimeSlots.Any(s => s.IsAvailable);
            result.Add(daySlot);
        }

        return result;
    }
}
