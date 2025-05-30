namespace Application.Modules.Meetings.Commands;

public class JoinMeetingCommandHandler(IApplicationDbContext dbContext, IMeetingService meetingService)
    : IRequestHandler<JoinMeetingCommand, MeetingJoinInfoDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly IMeetingService _meetingService = meetingService;

    public async Task<MeetingJoinInfoDto> Handle(JoinMeetingCommand command, CancellationToken cancellationToken)
    {
        Meeting? meeting = null;
        PetApplication? application = null;

        if (command.MeetingId.HasValue)
        {
            var meetingId = command.MeetingId.Value;
            meeting = await _dbContext.Meetings
                .AsNoTracking()
                .Include(m => m.Application)
                .FirstOrDefaultAsync(m => m.Id == meetingId, cancellationToken)
                ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundMeeting, $"Meeting with id {meetingId} not found");

            application = meeting.Application;
        }
        else
        {
            var applicationId = command.ApplicationId!.Value;
            application = await _dbContext.Applications
                .AsNoTracking()
                .Include(a => a.Meeting)
                .FirstOrDefaultAsync(a => a.Id == applicationId, cancellationToken)
                ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundApplication, $"Application with id {applicationId} not found");

            if (application.Meeting is null)
                throw new NotFoundException(Constants.ResponseCodes.NotFoundMeeting, $"Meeting was not scheduled for application with id {applicationId}");

            meeting = application.Meeting;
        }

        // todo: temporarily disabled, remove!!!
        //var currentDate = DateTime.UtcNow;
        //if (currentDate < meeting.Start.AddMinutes(5))
        //    throw new ConflictException("Meeting cannot be joined yet");

        //if (currentDate > meeting.End)
        //    throw new ConflictException("Meeting has already ended and cannot be joined");

        if (meeting.AdminId != _dbContext.User!.Id && application.UserId != _dbContext.User.Id)
            throw new ForbiddenException();

        var roomName = await _meetingService.GetRoomAsync(meeting.Id);
        var (Url, RoomName, Token) = _meetingService.GenerateJoinInfo(roomName, _dbContext.User);

        var result = new MeetingJoinInfoDto
        {
            Url = Url,
            RoomName = RoomName,
            Token = Token
        };

        return result;
    }
}
