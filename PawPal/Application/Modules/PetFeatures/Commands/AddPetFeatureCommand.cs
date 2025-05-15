namespace Application.Modules.PetFeatures.Commands;

public class AddPetFeatureCommand : IRequest<int>
{
    public string Feature { get; set; }
}
