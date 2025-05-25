namespace Tests.Modules.Applications.Fixtures;

public static class ApplicationFixtures
{
    public static PetApplication FakeApplicationEntity(
        int id,
        int userId,
        int petId,
        ApplicationStatus? status = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null)
    {
        var faker = new Faker<PetApplication>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => id)
            .RuleFor(x => x.Status, f => status ?? f.PickRandom<ApplicationStatus>())
            .RuleFor(x => x.UserId, f => userId)
            .RuleFor(x => x.User, f => null!)
            .RuleFor(x => x.PetId, f => petId)
            .RuleFor(x => x.Pet, f => null!)
            .RuleFor(x => x.Meeting, f => null)
            .RuleFor(x => x.CreatedAt, f => createdAt ?? DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, f => updatedAt ?? DateTime.UtcNow)
            .RuleFor(x => x.DeletedAt, f => deletedAt);

        faker.Validate();

        return faker.Generate();
    }

    public static SubmitApplicationCommand FakeSubmitApplicationCommand(int petId)
    {
        var faker = new Faker<SubmitApplicationCommand>()
            .StrictMode(true)
            .RuleFor(x => x.PetId, f => petId);

        faker.Validate();

        return faker.Generate();
    }

    public static ChangeApplicationStatusCommand FakeChangeApplicationStatusCommand(
        int applicationId,
        ApplicationStatus status)
    {
        var faker = new Faker<ChangeApplicationStatusCommand>()
            .StrictMode(true)
            .RuleFor(x => x.ApplicationId, f => applicationId)
            .RuleFor(x => x.Status, status);

        faker.Validate();

        return faker.Generate();
    }

    public static GetApplicationsFilteredQuery FakeGetApplicationsFilteredQuery(
        List<ApplicationStatus>? statuses = null,
        int page = 1,
        int pageSize = 10)
    {
        var faker = new Faker<GetApplicationsFilteredQuery>()
            .StrictMode(true)
            .RuleFor(x => x.Statuses, f => statuses)
            .RuleFor(x => x.Pagination, f => new PaginationDto() { Page = page, PageSize = pageSize });

        faker.Validate();

        return faker.Generate();
    }
}
