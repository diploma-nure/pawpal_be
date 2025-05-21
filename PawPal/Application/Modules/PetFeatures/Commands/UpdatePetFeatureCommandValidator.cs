namespace Application.Modules.PetFeatures.Commands;

public sealed class UpdatePetFeatureCommandValidator
    : AbstractValidator<UpdatePetFeatureCommand>
{
    public UpdatePetFeatureCommandValidator()
    {
        RuleFor(command => command.Id)
           .NotEmpty();

        RuleFor(command => command.Feature)
           .NotEmpty();
    }
}
