namespace Application.Modules.Users.Mappings;

public static class UserMappings
{
    public static UserInfoDto ToUserInfoDto(this User user)
        => new()
        {
            Id = user.Id,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePicture?.Url,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
        };

    public static SurveyDto ToSurveyDto(this Survey survey)
        => new()
        {
            Id = survey.Id,
            VacationPetCarePlan = survey.VacationPetCarePlan,
            HasOwnnedPetsBefore = survey.OwnerDetails.HasOwnnedPetsBefore,
            UnderstandsResponsibility = survey.OwnerDetails.UnderstandsResponsibility,
            HasSufficientFinancialResources = survey.OwnerDetails.HasSufficientFinancialResources,
            PlaceOfResidence = survey.ResidenceDetails.PlaceOfResidence,
            HasSafeWalkingArea = survey.ResidenceDetails.HasSafeWalkingArea,
            PetsAllowedAtResidence = survey.ResidenceDetails.PetsAllowedAtResidence,
            HasOtherPets = survey.ResidenceDetails.HasOtherPets,
            HasSmallChildren = survey.ResidenceDetails.HasSmallChildren,
            PreferredSpecies = survey.PetPreferences.PreferredSpecies,
            PreferredSizes = survey.PetPreferences.PreferredSizes,
            PreferredAges = survey.PetPreferences.PreferredAges,
            PreferredGenders = survey.PetPreferences.PreferredGenders,
            DesiredFeaturesIds = survey.PetPreferences.DesiredFeatures.Select(f => f.Id).ToList(),
            DesiredActivityLevel = survey.PetPreferences.DesiredActivityLevel,
            ReadyForSpecialNeedsPet = survey.PetPreferences.ReadyForSpecialNeedsPet,
        };
}