namespace Application.Modules.Users.Commands;

public class UpdateUserInfoCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateUserInfoCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
    {
        var userId = _dbContext.User!.Id;

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == _dbContext.User!.Id, cancellationToken)
            ?? throw new NotFoundException($"User with id {userId} not found");

        user.FullName = command.FullName;
        user.PhoneNumber = command.PhoneNumber;
        user.Address = command.Address;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
