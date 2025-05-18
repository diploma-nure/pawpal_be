namespace Domain.Entities;

public class Comment : IAuditable
{
    public int Id { get; set; }

    public string Value { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int? PetId { get; set; }

    public Pet? Pet { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
