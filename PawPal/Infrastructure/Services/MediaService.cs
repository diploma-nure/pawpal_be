namespace Infrastructure.Services;

public class MediaService(IAmazonS3 s3client, IOptions<StorageConfig> storageConfigOptions) : IMediaService
{
    private readonly IAmazonS3 _s3Client = s3client;

    private readonly StorageConfig _storageConfig = storageConfigOptions.Value;

    public async Task<UploadFileResponse> UploadPetPictureAsync(int petId, IFormFile file)
    {
        if (file.Length == 0)
            throw new ConflictException(Constants.ResponseCodes.ConflictEmptyFileData, $"File {file.FileName} is empty");

        var extension = Path.GetExtension(file.FileName);
        var key = $"{Constants.Media.PetFolderPrefix}-{petId}/{Guid.NewGuid()}{extension}";

        var transferUtility = new TransferUtility(_s3Client);
        using var stream = file.OpenReadStream();

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            BucketName = _storageConfig.Bucket,
            Key = key,
            ContentType = file.ContentType,
            CannedACL = S3CannedACL.PublicRead,
        };

        await transferUtility.UploadAsync(uploadRequest);

        var url = $"{_storageConfig.Url}/{_storageConfig.Bucket}/{Uri.EscapeDataString(key)}";
        var result = new UploadFileResponse
        {
            Url = url,
            Path = key,
            Source = FileSource.Cloud,
        };

        return result;
    }

    public async Task DeletePictureAsync(Picture picture)
    {
        if (picture.Source is FileSource.Internal)
        {
            if (File.Exists(picture.Path))
                File.Delete(picture.Path);
        }
        else if (picture.Source is FileSource.Cloud)
        {
            await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _storageConfig.Bucket,
                Key = picture.Path,
            });
        }
    }
}
