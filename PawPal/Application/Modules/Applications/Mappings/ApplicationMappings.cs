namespace Application.Modules.Applications.Mappings;

public static class ApplicationMappings
{
    public static ApplicationInListDto ToApplicationInListDto(this PetApplication application)
        => new()
        {
            Id = application.Id,
            Status = application.Status,
            CreatedAt = application.CreatedAt,
            UserId = application.UserId,
            UserFullName = application.User.FullName ?? application.User.Email,
            PetId = application.PetId,
            PetName = application.Pet.Name,
            PetPictureUrl = application.Pet.Pictures?.OrderBy(p => p.Order).FirstOrDefault()?.Url,
        };
}