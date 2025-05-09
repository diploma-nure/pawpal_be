namespace Application.Modules.Applications.Commands;

public class ChangeApplicationStatusCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ChangeApplicationStatusCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(ChangeApplicationStatusCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var application = await _dbContext.Applications
            .FirstOrDefaultAsync(p => p.Id == command.ApplicationId, cancellationToken)
            ?? throw new NotFoundException($"Application with id {command.ApplicationId} not found");

        application.Status = command.Status!.Value;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return application.Id;
    }
}
