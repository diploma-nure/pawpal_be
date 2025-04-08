namespace Domain.Entities;

public class User : IAuditable
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string? PasswordHash { get; set; }

    public Role Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
