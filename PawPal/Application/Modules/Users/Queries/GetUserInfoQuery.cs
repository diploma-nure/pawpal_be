namespace Application.Modules.Users.Queries;

public class GetUserInfoQuery : IRequest<UserInfoDto>
{
    public int? Id { get; set; }

    public GetUserInfoQuery()
    {
    }

    public GetUserInfoQuery(int id)
    {
        Id = id;
    }
}
