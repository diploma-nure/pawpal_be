namespace Application.Modules.Meetings.Commands;

public class ChangeMeetingStatusCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ChangeMeetingStatusCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(ChangeMeetingStatusCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var meeting = await _dbContext.Meetings
            .FirstOrDefaultAsync(p => p.Id == command.MeetingId, cancellationToken)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundMeeting, $"Meeting with id {command.MeetingId} not found");

        if (meeting.Status is MeetingStatus.Cancelled or MeetingStatus.Completed)
            throw new ConflictException(Constants.ResponseCodes.ConflictMeetingAlreadyCompleted, "Meeting is already cancelled or completed");

        meeting.Status = command.Status!.Value;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return meeting.Id;
    }
}
