namespace Application.Modules.Pets.Dtos;

public class PetInListDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PetSpecies Species { get; set; }

    public PetGender Gender { get; set; }

    public PetSize Size { get; set; }

    public PetAge Age { get; set; }

    public bool HasSpecialNeeds { get; set; }

    public string? Description { get; set; }

    public string? PictureUrl { get; set; }

    public decimal? MatchPercentage { get; set; }
}
