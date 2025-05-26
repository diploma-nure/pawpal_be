namespace Application.Modules.Pets.Dtos;

public class PetShortDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? PictureUrl { get; set; }

    public PetSpecies Species { get; set; }
}
