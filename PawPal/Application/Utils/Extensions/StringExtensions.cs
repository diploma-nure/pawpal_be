namespace Application.Utils.Extensions;

public static class StringExtensions
{
    public static string ToSha256Hash(this string rawData)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    public static string ToNormalizedEmail(this string email)
        => email.ToLowerInvariant();
}
