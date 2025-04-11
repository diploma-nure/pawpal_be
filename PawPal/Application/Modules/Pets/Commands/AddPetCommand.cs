namespace Application.Modules.Pets.Commands;

public class AddPetCommand : IRequest<int>
{
    public string? Name { get; set; }

    public PetSpecies? Species { get; set; }

    public PetGender? Gender { get; set; }

    public PetSize? Size { get; set; }

    public PetAge? Age { get; set; }

    public bool? HasSpecialNeeds { get; set; }

    public List<int>? FeaturesIds { get; set; }

    public string? Description { get; set; }

    public List<IFormFile>? Pictures { get; set; }
}
