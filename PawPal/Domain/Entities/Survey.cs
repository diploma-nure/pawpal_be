namespace Domain.Entities;

public class Survey : IAuditable
{
    public int Id { get; set; }

    public string? VacationPetCarePlan { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int OwnerDetailsId { get; set; }

    public SurveyOwnerDetails OwnerDetails { get; set; }

    public int ResidenceDetailsId { get; set; }

    public SurveyResidenceDetails ResidenceDetails { get; set; }

    public int PetPreferencesId { get; set; }

    public SurveyPetPreferences PetPreferences { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
