namespace Domain.Entities;

public class PetLike : IAuditable
{
    public int UserId { get; set; }

    public int PetId { get; set; }

    public User User { get; set; }

    public Pet Pet { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
