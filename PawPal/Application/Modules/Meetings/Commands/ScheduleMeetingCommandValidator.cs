namespace Application.Modules.Meetings.Commands;

public sealed class ScheduleMeetingCommandValidator
    : AbstractValidator<ScheduleMeetingCommand>
{
    public ScheduleMeetingCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .NotEmpty();

        RuleFor(command => command.Start)
            .NotEmpty();

        RuleFor(command => command.End)
            .NotEmpty();
    }
}
