namespace Domain.Entities;

public class PetApplication : IAuditable, ISoftDeletable
{
    public int Id { get; set; }

    public ApplicationStatus Status { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int PetId { get; set; }

    public Pet Pet { get; set; }

    public Meeting? Meeting { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
