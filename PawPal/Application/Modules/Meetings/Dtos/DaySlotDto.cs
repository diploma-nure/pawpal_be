namespace Application.Modules.Meetings.Dtos;

public class DaySlotDto
{
    public DateOnly Date { get; set; }

    public bool IsAvailable { get; set; }

    public List<TimeSlotDto> TimeSlots { get; set; }
}
