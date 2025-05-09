namespace Application.Modules.Meetings.Dtos;

public class MeetingInListDto
{
    public int Id { get; set; }

    public MeetingStatus Status { get; set; }

    public DateTime Start { get; set; }

    public UserShortDto User { get; set; }

    public PetShortDto Pet { get; set; }

    public ApplicationShortDto Application { get; set; }
}
