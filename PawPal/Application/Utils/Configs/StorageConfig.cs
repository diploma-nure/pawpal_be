namespace Application.Utils.Configs;

public class StorageConfig
{
    public const string Storage = "Storage";

    public required string Bucket { get; set; }
    
    public required string Url { get; set; }
    
    public required string AccessKey { get; set; }

    public required string Secret { get; set; }
}
