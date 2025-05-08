namespace Application.Modules.Applications.Dtos;

public class ApplicationInListDto
{
    public int Id { get; set; }

    public ApplicationStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public string UserFullName { get; set; }

    public int PetId { get; set; }

    public string PetName { get; set; }

    public string? PetPictureUrl { get; set; }
}
