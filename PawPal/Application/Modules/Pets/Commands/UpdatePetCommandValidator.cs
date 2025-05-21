namespace Application.Modules.Pets.Commands;

public sealed class UpdatePetCommandValidator
    : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(command => command.Id)
           .NotEmpty();

        RuleFor(command => command.Species)
            .IsInEnum()
            .When(command => command.Species is not null);

        RuleFor(command => command.Gender)
            .IsInEnum()
            .When(command => command.Gender is not null);

        RuleFor(command => command.Size)
            .IsInEnum()
            .When(command => command.Size is not null);

        RuleFor(command => command.Age)
            .IsInEnum()
            .When(command => command.Age is not null);

        RuleFor(command => command.FeaturesIds)
            .ForEach(feature => feature.NotEmpty());

        RuleFor(command => command.Pictures)
            .Must(pictures => pictures == null || pictures.All(p => p.File == null || p.File.ContentType.StartsWith("image/")))
            .WithMessage("All uploaded files must be images.");
    }
}
