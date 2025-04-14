namespace Application.Utils.Configs;

public class EmailConfig
{
    public const string Email = "Email";

    public required string EmailId { get; init; }

    public required string Name { get; init; }

    public required string Password { get; init; }

    public required string Host { get; init; }

    public required int Port { get; init; }

    public required bool UseSSL { get; init; }

    public required EmailTemplatesConfig Templates { get; init; }
}
