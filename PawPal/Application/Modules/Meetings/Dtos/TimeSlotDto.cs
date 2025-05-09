namespace Application.Modules.Meetings.Dtos;

public class TimeSlotDto
{
    public bool IsAvailable { get; set; }

    public TimeOnly Time { get; set; }
}
