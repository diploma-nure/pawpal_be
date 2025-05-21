namespace Application.Interfaces;

public interface IMediaService
{
    Task<UploadFileResponse> UploadPetPictureAsync(int petId, IFormFile file);

    Task DeletePictureAsync(Picture picture);
}

public class UploadFileResponse
{
    public string Url { get; set; }

    public string Path { get; set; }

    public FileSource Source { get; set; }
}