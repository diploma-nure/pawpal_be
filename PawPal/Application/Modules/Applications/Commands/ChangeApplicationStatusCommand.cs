namespace Application.Modules.Applications.Commands;

public class ChangeApplicationStatusCommand : IRequest<int>
{
    public int ApplicationId { get; set; }

    public ApplicationStatus? Status { get; set; }
}
