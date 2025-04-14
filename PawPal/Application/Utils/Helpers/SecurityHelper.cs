namespace Application.Utils.Helpers;

public class SecurityHelper
{
    public static string GenerateRecoveryCode()
    {
        var randomNumber = new byte[4];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        var value = BitConverter.ToInt32(randomNumber, 0);
        value = Math.Abs(value % 1000000);
        return value.ToString("D6");
    }
}
