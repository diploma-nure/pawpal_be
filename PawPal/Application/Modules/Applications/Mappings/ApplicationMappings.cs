namespace Application.Modules.Applications.Mappings;

public static class ApplicationMappings
{
    public static ApplicationInListDto ToApplicationInListDto(this PetApplication application)
        => new()
        {
            Id = application.Id,
            Status = application.Status,
            CreatedAt = application.CreatedAt,
            User = application.User.ToUserShortDto(),
            Pet = application.Pet.ToPetShortDto(),
            MeetingId = application.Meeting?.Id,
        };

    public static ApplicationShortDto ToApplicationShortDto(this PetApplication application)
        => new()
        {
            Id = application.Id,
        };
}