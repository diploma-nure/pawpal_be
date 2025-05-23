namespace Domain.Entities;

public class User : IAuditable, ISoftDeletable
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string? PasswordHash { get; set; }

    public Role Role { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? PasswordRecoveryCode { get; set; }

    public List<PetLike> PetLikes { get; set; }

    public List<PetApplication> Applications { get; set; }

    public List<Meeting> Meetings { get; set; }

    public Picture? ProfilePicture { get; set; }

    public Survey? Survey { get; set; }

    public List<Comment> Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
