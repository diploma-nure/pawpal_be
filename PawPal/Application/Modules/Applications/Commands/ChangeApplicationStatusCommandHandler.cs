namespace Application.Modules.Applications.Commands;

public class ChangeApplicationStatusCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    : IRequestHandler<ChangeApplicationStatusCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(ChangeApplicationStatusCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var application = await _dbContext.Applications
            .Include(a => a.Pet)
            .Include(a => a.Meeting)
            .FirstOrDefaultAsync(p => p.Id == command.ApplicationId, cancellationToken)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundApplication, $"Application with id {command.ApplicationId} not found");

        if (application.Status is ApplicationStatus.Approved or ApplicationStatus.Rejected)
            throw new ConflictException(Constants.ResponseCodes.ConflictApplicationAlreadyApproved, "Application is already approved or rejected");

        var newStatus = command.Status!.Value;
        application.Status = newStatus;
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (newStatus is ApplicationStatus.Approved)
            await mediator.Send(new DeletePetCommand(application.PetId), cancellationToken);
        else if (newStatus is ApplicationStatus.Rejected && application.Meeting?.Status is MeetingStatus.Scheduled)
                application.Meeting.Status = MeetingStatus.Cancelled;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return application.Id;
    }
}
