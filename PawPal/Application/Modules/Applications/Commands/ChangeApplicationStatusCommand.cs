namespace Application.Modules.Applications.Commands;

public class ChangeApplicationStatusCommand(int applicationId) : IRequest<int>
{
    public int ApplicationId { get; set; } = applicationId;

    public ApplicationStatus? Status { get; set; }
}
