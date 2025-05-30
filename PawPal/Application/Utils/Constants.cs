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

    public static class ResponseCodes
    {
        public const string Success = "S001";

        public const string Error = "E001";

        public const string ConflictUserExists = "C001";
        public const string ConflictApplicationAlreadyApproved = "C002";
        public const string ConflictNoRecoveryCodeForUser = "C003";
        public const string ConflictInvalidRecoveryCode = "C004";
        public const string ConflictPasswordsNotMatch = "C005";
        public const string ConflictMeetingAlreadyCompleted = "C006";
        public const string ConflictMeetingRescheduleForbiddenForMeetingStatus = "C007";
        public const string ConflictMeetingScheduleForbiddenForApplicationStatus = "C008";
        public const string ConflictBadScheduleDate = "C009";
        public const string ConflictBadScheduleTime = "C010";
        public const string ConflictNoAvailableAdmins = "C011";
        public const string ConflictEmptyFileData = "C012";
        public const string ConflictHubSendingFailed = "C013";
        public const string ConflictHubJoinFailed = "C014";

        public const string NotFoundApplication = "NF001";
        public const string NotFoundPet = "NF002";
        public const string NotFoundPetFeature = "NF003";
        public const string NotFoundMeeting = "NF004";
        public const string NotFoundSurvey = "NF005";
        public const string NotFoundUser = "NF006";
        public const string NotFoundComment = "NF007";

        public const string AuthUnauthorized = "A001";
        public const string AuthForbidden = "A002";
        public const string AuthLoginFailed = "A003";

        public const string ValidationFailed = "V001";
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
