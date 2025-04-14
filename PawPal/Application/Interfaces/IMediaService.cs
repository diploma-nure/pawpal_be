namespace Application.Interfaces;

public interface IMediaService
{
    Task<(string Url, string Path)> UploadPetPictureAsync(int petId, IFormFile file);
}
