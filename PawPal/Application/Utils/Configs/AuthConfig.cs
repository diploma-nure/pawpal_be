namespace Application.Utils.Configs;

public class AuthConfig
{
    public const string Auth = "Auth";

    public required string Secret { get; init; }
}
