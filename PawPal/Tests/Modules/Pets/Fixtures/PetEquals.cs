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

    public static void EqualTo(this Pet entity, PetInListDto dto)
    {
        entity.Id.Should().Be(dto.Id);
        entity.Name.Should().Be(dto.Name);
        entity.Species.Should().Be(dto.Species);
        entity.Gender.Should().Be(dto.Gender);
        entity.Size.Should().Be(dto.Size);
        entity.Age.Should().Be(dto.Age);
        entity.HasSpecialNeeds.Should().Be(dto.HasSpecialNeeds);
        entity.Description.Should().Be(dto.Description);

        entity.Pictures.Select(p => p.Url).FirstOrDefault().Should().BeEquivalentTo(dto.PictureUrl);
    }

    public static void EqualTo(this Pet entity, AddPetCommand command)
    {
        entity.Name.Should().Be(command.Name);
        entity.Species.Should().Be(command.Species);
        entity.Gender.Should().Be(command.Gender);
        entity.Size.Should().Be(command.Size);
        entity.Age.Should().Be(command.Age);
        entity.HasSpecialNeeds.Should().Be(command.HasSpecialNeeds!.Value);
        entity.Description.Should().Be(command.Description);

        entity.Features.Select(f => f.Id).Should().BeEquivalentTo(command.FeaturesIds);
    }

    public static void EqualTo(this Pet entity, UpdatePetCommand command)
    {
        entity.Name.Should().Be(command.Name);
        entity.Species.Should().Be(command.Species);
        entity.Gender.Should().Be(command.Gender);
        entity.Size.Should().Be(command.Size);
        entity.Age.Should().Be(command.Age);
        entity.HasSpecialNeeds.Should().Be(command.HasSpecialNeeds!.Value);
        entity.Description.Should().Be(command.Description);

        entity.Features.Select(f => f.Id).Should().BeEquivalentTo(command.FeaturesIds);
    }
}
