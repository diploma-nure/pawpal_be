namespace Application.Modules.Meetings.Queries;

public class GetMeetingSlotsQuery : IRequest<List<DaySlotDto>>
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
