namespace Domain.Entities;

public class SurveyOwnerDetails : IAuditable
{
    public int Id { get; set; }

    public bool HasOwnnedPetsBefore { get; set; }

    public bool UnderstandsResponsibility { get; set; }

    public bool HasSufficientFinancialResources { get; set; }

    public List<Survey> Surveys { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
