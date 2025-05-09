namespace Application.Modules.Pets.Commands;

public sealed class LikePetCommandValidator
    : AbstractValidator<LikePetCommand>
{
    public LikePetCommandValidator()
    {
        RuleFor(command => command.PetId)
            .NotEmpty();
    }
}
