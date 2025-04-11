namespace Application.Modules.Users.Queries;

public class GetUserInfoQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetUserInfoQuery, UserInfoDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<UserInfoDto> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        UserInfoDto? result = null;

        if (query.Id is null)
        {
            result = (_dbContext.User ?? throw new UnauthorizedException()).ToUserInfoDto();
            return result;
        }

        if (_dbContext.User!.Role is not Role.Admin)
            throw new ForbiddenException();

        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == query.Id, cancellationToken)
            ?? throw new NotFoundException($"User with id {query.Id} not found");

        result = user.ToUserInfoDto();
        return result;
    }
}
