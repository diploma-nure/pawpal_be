namespace Application.Modules.Pets.Commands;

public class LikePetCommand(int petId) : IRequest<int>
{
    public int PetId { get; set; } = petId;
}
