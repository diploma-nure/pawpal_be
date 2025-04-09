namespace Application.Modules.Pets.Commands;

public class AddPetCommand : IRequest<int>
{
    public string? Name { get; set; }

    public AnimalGender? Gender { get; set; }

    public AnimalSize? Size { get; set; }

    public int? AgeYears { get; set; }

    public int? AgeMonths { get; set; }

    public string? Breed { get; set; }

    public bool? HasSpecialNeeds { get; set; }

    public List<string>? Features { get; set; }

    public string? Description { get; set; }
}
