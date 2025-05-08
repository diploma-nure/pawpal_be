namespace Application.Modules.Applications.Commands;

public sealed class SubmitApplicationCommandValidator
    : AbstractValidator<SubmitApplicationCommand>
{
    public SubmitApplicationCommandValidator()
    {
        RuleFor(command => command.PetId)
            .NotEmpty();
    }
}
