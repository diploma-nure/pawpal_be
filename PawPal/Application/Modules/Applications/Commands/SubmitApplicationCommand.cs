namespace Application.Modules.Applications.Commands;

public class SubmitApplicationCommand : IRequest<int>
{
    public int PetId { get; set; }
}
