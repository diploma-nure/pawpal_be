namespace Application.Modules.Pets.Commands;

public class UnlikePetCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
