namespace Tests.Modules.Users.Fixtures;

public static class UserFixtures
{
    public static User FakeUserEntity(int id, Role role = Role.User)
    {
        var faker = new Faker<User>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => id)
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
            .RuleFor(x => x.Role, f => role)
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.PasswordRecoveryCode, f => null)
            .RuleFor(x => x.PetLikes, f => [])
            .RuleFor(x => x.Applications, f => [])
            .RuleFor(x => x.Meetings, f => [])
            .RuleFor(x => x.ProfilePicture, f => null)
            .RuleFor(x => x.Survey, f => null)
            .RuleFor(x => x.Comments, f => [])
            .RuleFor(x => x.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.DeletedAt, f => null);

        faker.Validate();

        return faker.Generate();
    }
}
