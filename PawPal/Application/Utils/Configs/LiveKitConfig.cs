namespace Application.Utils.Configs;

public class LiveKitConfig
{
    public const string LiveKit = "LiveKit";

    public required string Url { get; init; }

    public required string ApiKey { get; init; }

    public required string ApiSecret { get; init; }
}
