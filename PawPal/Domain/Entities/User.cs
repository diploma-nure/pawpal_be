namespace Domain.Entities;

public class User : IAuditable
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string? PasswordHash { get; set; }

    public Role Role { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public List<PetLike> PetLikes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
