namespace Application.Utils;

public static class Constants
{
    public static class Claims
    {
        public const string IdClaim = "id";
        public const string EmailClaim = "email";
        public const string RoleClaim = "role";
    }

    public static class Roles
    {
        public const string None = "None";
        public const string User = "User";
        public const string Admin = "Admin";
    }

    public static class Defaults
    {
        public const string SuccessMessage = "Success";
    }

    public static class Media
    {
        public const string PetFolderPrefix = "pet";
    }

    public static class Patterns
    {
        public const string PhoneNumber = @"^\+?[1-9]\d{1,14}$";
        public const string Email = @"^(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*)@(?:(?:[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,})$";
    }

    public static class TimePeriods
    {
        public static readonly TimeSpan MeetingStatusCheck = TimeSpan.FromMinutes(10);
    }
}
