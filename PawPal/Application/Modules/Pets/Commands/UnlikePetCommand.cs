namespace Application.Modules.Pets.Commands;

public class UnlikePetCommand(int petId) : IRequest<int>
{
    public int PetId { get; set; } = petId;
}
