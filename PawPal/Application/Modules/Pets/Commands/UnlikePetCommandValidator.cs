namespace Application.Modules.Pets.Commands;

public sealed class UnlikePetCommandValidator
    : AbstractValidator<UnlikePetCommand>
{
    public UnlikePetCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
