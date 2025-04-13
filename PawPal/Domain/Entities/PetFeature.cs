namespace Domain.Entities;

public class PetFeature : IAuditable
{
    public int Id { get; set; }

    public string Feature { get; set; }

    public List<Pet> Pets { get; set; }

    public List<SurveyPetPreferences> SurveysPetPreferences { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
