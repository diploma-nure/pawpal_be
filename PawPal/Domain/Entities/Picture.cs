namespace Domain.Entities;

public class Picture : IAuditable
{
    public int Id { get; set; }

    public FileSource Source { get; set; }

    public string Url { get; set; }

    public string? Path { get; set; }

    public int Order { get; set; }

    public int? UserId { get; set; }

    public User? User { get; set; }

    public int? PetId { get; set; }

    public Pet? Pet { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
