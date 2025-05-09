namespace Application.Modules.Pets.Dtos;

public class PetRecommendationDto
{
    [Column("pet_id")]
    public int PetId { get; set; }

    [Column("match_percentage")]
    public decimal MatchPercentage { get; set; }
}