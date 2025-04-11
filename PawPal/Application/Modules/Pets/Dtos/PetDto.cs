namespace Application.Modules.Pets.Dtos;

public class PetDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PetSpecies Species { get; set; }

    public PetGender Gender { get; set; }

    public PetSize Size { get; set; }

    public PetAge Age { get; set; }

    public bool HasSpecialNeeds { get; set; }

    public List<string>? Features { get; set; }

    public string? Description { get; set; }

    public List<string>? PicturesUrls { get; set; }
}
