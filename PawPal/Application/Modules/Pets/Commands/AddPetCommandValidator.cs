namespace Application.Modules.Pets.Commands;

public sealed class AddPetCommandValidator
    : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty();

        RuleFor(command => command.Species)
            .NotNull()
            .IsInEnum();

        RuleFor(command => command.Gender)
            .NotNull()
            .IsInEnum();

        RuleFor(command => command.Size)
            .NotNull()
            .IsInEnum();

        RuleFor(command => command.Age)
            .NotNull()
            .IsInEnum();

        RuleFor(command => command.HasSpecialNeeds)
            .NotNull();

        RuleFor(command => command.FeaturesIds)
            .ForEach(feature => feature.NotEmpty());

        RuleFor(command => command.Pictures)
            .Must(pictures => pictures == null || pictures.All(p => p.ContentType.StartsWith("image/")))
            .WithMessage("All uploaded files must be images.");
    }
}
