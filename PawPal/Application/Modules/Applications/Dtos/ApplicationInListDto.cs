namespace Application.Modules.Applications.Dtos;

public class ApplicationInListDto
{
    public int Id { get; set; }

    public ApplicationStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserShortDto User { get; set; }

    public PetShortDto Pet { get; set; }

    public int? MeetingId { get; set; }
}
