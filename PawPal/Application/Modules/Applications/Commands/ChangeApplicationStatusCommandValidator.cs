namespace Application.Modules.Applications.Commands;

public sealed class ChangeApplicationStatusCommandValidator
    : AbstractValidator<ChangeApplicationStatusCommand>
{
    public ChangeApplicationStatusCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .NotEmpty();

        RuleFor(command => command.Status)
            .NotNull()
            .IsInEnum();
    }
}
