namespace Application.Modules.PetFeatures.Commands;

public class DeletePetFeatureCommand(int petFeatureId) : IRequest<int>
{
    public int PetFeatureId { get; set; } = petFeatureId;
}
