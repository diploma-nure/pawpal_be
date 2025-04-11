namespace Application.Interfaces;

public interface IMediaService
{
    Task<string> UploadPetPictureAsync(int petId, IFormFile file);
}
