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
            .NotEmpty()
            .Must((command, end) =>
                command.Start.HasValue && end.HasValue && end > command.Start
            )
            .WithMessage("End date must be after start date.");
    }
}
