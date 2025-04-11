namespace Application.Modules.Pets.Commands;

public class LikePetCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
