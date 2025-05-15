namespace Application.Modules.Pets.Commands;

public sealed class DeletePetCommandValidator
    : AbstractValidator<DeletePetCommand>
{
    public DeletePetCommandValidator()
    {
        RuleFor(query => query.PetId)
            .NotEmpty();
    }
}
