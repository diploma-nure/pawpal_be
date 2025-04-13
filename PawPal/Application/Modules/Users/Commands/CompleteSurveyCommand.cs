namespace Application.Modules.Users.Commands;

public class CompleteSurveyCommand : IRequest<int>
{
    public string? VacationPetCarePlan { get; set; }

    // Owner Details
    public bool? HasOwnnedPetsBefore { get; set; }

    public bool? UnderstandsResponsibility { get; set; }

    public bool? HasSufficientFinancialResources { get; set; }

    // Residence Details
    public PlaceOfResidence? PlaceOfResidence { get; set; }

    public bool? HasSafeWalkingArea { get; set; }

    public Selection? PetsAllowedAtResidence { get; set; }

    public bool? HasOtherPets { get; set; }

    public bool? HasSmallChildren { get; set; }

    // Pet Preferences
    public List<PetSpecies>? PreferredSpecies { get; set; }

    public List<PetSize>? PreferredSizes { get; set; }

    public List<PetAge>? PreferredAges { get; set; }

    public List<PetGender>? PreferredGenders { get; set; }

    public List<int>? DesiredFeaturesIds { get; set; }

    public PetActivity? DesiredActivityLevel { get; set; }

    public bool? ReadyForSpecialNeedsPet { get; set; }
}
