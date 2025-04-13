namespace Application.Modules.Users.Commands;

public sealed class CompleteSurveyCommandValidator
    : AbstractValidator<CompleteSurveyCommand>
{
    public CompleteSurveyCommandValidator()
    {
        RuleFor(command => command.HasOwnnedPetsBefore)
            .NotNull();

        RuleFor(command => command.UnderstandsResponsibility)
            .NotNull();

        RuleFor(command => command.HasSufficientFinancialResources)
            .NotNull();

        RuleFor(command => command.PlaceOfResidence)
            .NotNull();

        RuleFor(command => command.HasSafeWalkingArea)
            .NotNull();

        RuleFor(command => command.PetsAllowedAtResidence)
            .NotNull();

        RuleFor(command => command.HasOtherPets)
            .NotNull();

        RuleFor(command => command.HasSmallChildren)
            .NotNull();

        RuleFor(command => command.PreferredSpecies)
            .ForEach(species => species.IsInEnum())
            .When(command => command.PreferredSpecies is not null);

        RuleFor(command => command.PreferredSizes)
            .ForEach(sizes => sizes.IsInEnum())
            .When(command => command.PreferredSizes is not null);

        RuleFor(command => command.PreferredAges)
            .ForEach(ages => ages.IsInEnum())
            .When(command => command.PreferredAges is not null);

        RuleFor(command => command.PreferredGenders)
            .ForEach(gender => gender.IsInEnum())
            .When(command => command.PreferredGenders is not null);

        RuleFor(command => command.DesiredActivityLevel)
            .NotNull();

        RuleFor(command => command.ReadyForSpecialNeedsPet)
            .NotNull();
    }
}
