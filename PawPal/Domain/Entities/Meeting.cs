namespace Domain.Entities;

public class Meeting : IAuditable
{
    public int Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public MeetingStatus Status { get; set; }

    public int AdminId { get; set; }

    public User Admin { get; set; }

    public int ApplicationId { get; set; }

    public PetApplication Application { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
