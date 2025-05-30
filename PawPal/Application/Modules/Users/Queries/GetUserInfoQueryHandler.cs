namespace Application.Modules.Users.Queries;

public class GetUserInfoQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetUserInfoQuery, UserInfoDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<UserInfoDto> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        UserInfoDto? result = null;

        if (query.UserId is null)
        {
            result = (_dbContext.User ?? throw new UnauthorizedException()).ToUserInfoDto();
            return result;
        }

        var user = await _dbContext.Users
            .Include(u => u.ProfilePicture)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundUser, $"User with id {query.UserId} not found");

        if (_dbContext.User!.Role is not Role.Admin && user.Id != _dbContext.User!.Id)
            throw new ForbiddenException();

        result = user.ToUserInfoDto();
        return result;
    }
}
