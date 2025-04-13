namespace Domain.Entities;

public class SurveyResidenceDetails : IAuditable
{
    public int Id { get; set; }

    public PlaceOfResidence PlaceOfResidence { get; set; }

    public bool HasSafeWalkingArea { get; set; }

    public Selection PetsAllowedAtResidence { get; set; }

    public bool HasOtherPets { get; set; }

    public bool HasSmallChildren { get; set; }

    public List<Survey> Surveys { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
