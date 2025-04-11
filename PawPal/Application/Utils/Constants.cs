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
        public const string PetsFolderPath = "pets";
        public const string PetsPicturesFolderPath = "pictures";
    }
}
