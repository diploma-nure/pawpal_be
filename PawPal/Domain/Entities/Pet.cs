namespace Domain.Entities;

public class Pet : IAuditable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PetSpecies Species { get; set; }

    public PetGender Gender { get; set; }

    public PetSize Size { get; set; }

    public PetAge Age { get; set; }

    public bool HasSpecialNeeds { get; set; }

    public List<PetFeature> Features { get; set; }

    public string? Description { get; set; }

    public List<PetLike> PetLikes { get; set; }

    public List<Picture> Pictures { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
