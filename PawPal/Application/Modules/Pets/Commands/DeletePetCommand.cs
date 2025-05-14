namespace Application.Modules.Pets.Commands;

public class DeletePetCommand(int petId) : IRequest<int>
{
    public int PetId { get; set; } = petId;
}
