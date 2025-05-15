namespace Application.Modules.PetFeatures.Commands;

public sealed class DeletePetFeatureCommandValidator
    : AbstractValidator<DeletePetFeatureCommand>
{
    public DeletePetFeatureCommandValidator()
    {
        RuleFor(query => query.PetFeatureId)
            .NotEmpty();
    }
}
