namespace Application.Modules.Users.Commands;

public class CompleteSurveyCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CompleteSurveyCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(CompleteSurveyCommand command, CancellationToken cancellationToken)
    {
        var survey = await _dbContext.Surveys.FirstOrDefaultAsync(s => s.UserId == _dbContext.User!.Id, cancellationToken);

        if (survey == null)
        {
            survey = new Survey
            {
                UserId = _dbContext.User!.Id,
                VacationPetCarePlan = command.VacationPetCarePlan,
                OwnerDetails = await GetOwnerDetailsAsync(command, cancellationToken),
                ResidenceDetails = await GetResidenceDetailsAsync(command, cancellationToken),
                PetPreferences = await GetPetPreferencesAsync(command, cancellationToken),
            };

            _dbContext.Surveys.Add(survey);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return survey.Id;
        }

        survey.VacationPetCarePlan = command.VacationPetCarePlan;
        survey.OwnerDetails = await GetOwnerDetailsAsync(command, cancellationToken);
        survey.ResidenceDetails = await GetResidenceDetailsAsync(command, cancellationToken);
        survey.PetPreferences = await GetPetPreferencesAsync(command, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return survey.Id;
    }

    private async Task<SurveyOwnerDetails> GetOwnerDetailsAsync(CompleteSurveyCommand command, CancellationToken cancellationToken)
    {
        var ownerDetails = await _dbContext.SurveysOwnerDetails
            .FirstOrDefaultAsync(od =>
                od.HasOwnnedPetsBefore == command.HasOwnnedPetsBefore &&
                od.UnderstandsResponsibility == command.UnderstandsResponsibility &&
                od.HasSufficientFinancialResources == command.HasSufficientFinancialResources
            , cancellationToken);

        if (ownerDetails is not null)
            return ownerDetails;

        ownerDetails = new SurveyOwnerDetails()
        {
            HasOwnnedPetsBefore = command.HasOwnnedPetsBefore!.Value,
            UnderstandsResponsibility = command.UnderstandsResponsibility!.Value,
            HasSufficientFinancialResources = command.HasSufficientFinancialResources!.Value,
        };

        return ownerDetails;
    }

    private async Task<SurveyResidenceDetails> GetResidenceDetailsAsync(CompleteSurveyCommand command, CancellationToken cancellationToken)
    {
        var residenceDetails = await _dbContext.SurveysResidenceDetails
            .FirstOrDefaultAsync(rd =>
                rd.PlaceOfResidence == command.PlaceOfResidence &&
                rd.HasSafeWalkingArea == command.HasSafeWalkingArea &&
                rd.PetsAllowedAtResidence == command.PetsAllowedAtResidence &&
                rd.HasOtherPets == command.HasOtherPets &&
                rd.HasSmallChildren == command.HasSmallChildren
            , cancellationToken);

        if (residenceDetails is not null)
            return residenceDetails;

        residenceDetails = new SurveyResidenceDetails()
        {
            PlaceOfResidence = command.PlaceOfResidence!.Value,
            HasSafeWalkingArea = command.HasSafeWalkingArea!.Value,
            PetsAllowedAtResidence = command.PetsAllowedAtResidence!.Value,
            HasOtherPets = command.HasOtherPets!.Value,
            HasSmallChildren = command.HasSmallChildren!.Value,
        };

        return residenceDetails;
    }

    private async Task<SurveyPetPreferences> GetPetPreferencesAsync(CompleteSurveyCommand command, CancellationToken cancellationToken)
        => new()
            {
                PreferredSpecies = command.PreferredSpecies,
                PreferredSizes = command.PreferredSizes,
                PreferredAges = command.PreferredAges,
                PreferredGenders = command.PreferredGenders,
                DesiredFeatures = await _dbContext.PetFeatures
                    .Where(f => command.DesiredFeaturesIds!.Contains(f.Id))
                    .ToListAsync(cancellationToken),
                DesiredActivityLevel = command.DesiredActivityLevel!.Value,
                ReadyForSpecialNeedsPet = command.ReadyForSpecialNeedsPet!.Value,
            };
}
