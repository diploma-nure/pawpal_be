namespace Application.Modules.Applications.Commands;

public class SubmitApplicationCommand(int petId) : IRequest<int>
{
    public int PetId { get; set; } = petId;
}
