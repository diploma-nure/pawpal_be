namespace Domain.Entities;

public class SurveyPetPreferences : IAuditable
{
    public int Id { get; set; }

    public List<PetSpecies>? PreferredSpecies { get; set; }

    public List<PetSize>? PreferredSizes { get; set; }

    public List<PetAge>? PreferredAges { get; set; }

    public List<PetGender>? PreferredGenders { get; set; }

    public List<PetFeature> DesiredFeatures { get; set; }

    public PetActivity DesiredActivityLevel { get; set; }

    public bool ReadyForSpecialNeedsPet { get; set; }

    public List<Survey> Surveys { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
