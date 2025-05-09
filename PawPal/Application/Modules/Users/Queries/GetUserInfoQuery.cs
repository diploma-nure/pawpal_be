namespace Application.Modules.Users.Queries;

public class GetUserInfoQuery : IRequest<UserInfoDto>
{
    public int? UserId { get; set; }
}
