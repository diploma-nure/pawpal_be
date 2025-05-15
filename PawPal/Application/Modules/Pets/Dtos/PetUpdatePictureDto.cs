namespace Application.Modules.Pets.Dtos;

public class PetUpdatePictureDto
{
    public int? Id { get; set; }

    public IFormFile? File { get; set; }
}
