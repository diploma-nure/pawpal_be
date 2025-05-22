namespace Tests.Modules.Pets.Fixtures;

public static class PetEquals
{
    public static void EqualTo(this Pet entity, PetDto dto)
    {
        entity.Id.Should().Be(dto.Id);
        entity.Name.Should().Be(dto.Name);
        entity.Species.Should().Be(dto.Species);
        entity.Gender.Should().Be(dto.Gender);
        entity.Size.Should().Be(dto.Size);
        entity.Age.Should().Be(dto.Age);
        entity.HasSpecialNeeds.Should().Be(dto.HasSpecialNeeds);
        entity.Description.Should().Be(dto.Description);

        entity.Features.Select(f => f.Feature).Should().BeEquivalentTo(dto.Features);

        entity.Pictures.Select(p => p.Url).Should().BeEquivalentTo(dto.Pictures?.Select(p => p.Url));
    }
}
