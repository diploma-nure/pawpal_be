namespace Infrastructure.Services;

public class MediaService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : IMediaService
{
    private readonly IWebHostEnvironment _env = env;

    private readonly HttpRequest _request = httpContextAccessor.HttpContext?.Request ?? throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext));

    public async Task<(string Url, string Path)> UploadPetPictureAsync(int petId, IFormFile file)
    {
        if (file.Length <= 0)
            throw new ConflictException($"File {file.FileName} is empty");

        var uploadFolder = Path.Combine(_env.WebRootPath, Constants.Media.PetsFolderPath, petId.ToString(), Constants.Media.PetsPicturesFolderPath);
        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        var extension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(uploadFolder, uniqueFileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var url = $"{_request.Scheme}://{_request.Host}/{Constants.Media.PetsFolderPath}/{petId}/{Constants.Media.PetsPicturesFolderPath}/{uniqueFileName}";
        return (url, filePath);
    }
}
