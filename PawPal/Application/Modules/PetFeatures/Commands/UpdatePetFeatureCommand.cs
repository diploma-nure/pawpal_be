namespace Application.Modules.PetFeatures.Commands;

public class UpdatePetFeatureCommand : IRequest<int>
{
    public int Id { get; set; }

    public string? Feature { get; set; }
}
