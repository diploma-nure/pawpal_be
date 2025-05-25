namespace Tests.Modules.Applications.Fixtures;

public static class ApplicationEquals
{
    public static void EqualTo(this PetApplication entity, SubmitApplicationCommand command)
    {
        entity.PetId.Should().Be(command.PetId);
    }

    public static void EqualTo(this PetApplication entity, ChangeApplicationStatusCommand command)
    {
        entity.Id.Should().Be(command.ApplicationId);
        entity.Status.Should().Be(command.Status);
    }
}
