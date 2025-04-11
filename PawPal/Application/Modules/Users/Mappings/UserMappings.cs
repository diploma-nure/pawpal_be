namespace Application.Modules.Users.Mappings;

public static class UserMappings
{
    public static UserInfoDto ToUserInfoDto(this User user)
        => new()
        {
            Id = user.Id,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
        };
}