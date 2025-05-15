namespace Application.Modules.PetFeatures.Commands;

public sealed class AddPetFeatureCommandValidator
    : AbstractValidator<AddPetFeatureCommand>
{
    public AddPetFeatureCommandValidator()
    {
        RuleFor(command => command.Feature)
            .NotEmpty();
    }
}
