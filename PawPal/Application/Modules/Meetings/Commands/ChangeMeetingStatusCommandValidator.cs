namespace Application.Modules.Meetings.Commands;

public sealed class ChangeMeetingStatusCommandValidator
    : AbstractValidator<ChangeMeetingStatusCommand>
{
    public ChangeMeetingStatusCommandValidator()
    {
        RuleFor(command => command.MeetingId)
            .NotEmpty();

        RuleFor(command => command.Status)
            .NotNull()
            .IsInEnum();
    }
}
