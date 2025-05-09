namespace Application.Modules.Meetings.Commands;

public sealed class JoinMeetingCommandValidator
   : AbstractValidator<JoinMeetingCommand>
{
    public JoinMeetingCommandValidator()
    {
        RuleFor(command => command)
            .Must(command => command.ApplicationId.HasValue || command.MeetingId.HasValue)
            .WithMessage("Either ApplicationId or MeetingId must be provided");
    }
}
