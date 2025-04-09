namespace Domain.Entities;

public class Pet : IAuditable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public AnimalGender Gender { get; set; }

    public AnimalSize Size { get; set; }

    public int AgeMonths { get; set; }

    public string? Breed { get; set; }

    public bool HasSpecialNeeds { get; set; }

    public string? Features { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
