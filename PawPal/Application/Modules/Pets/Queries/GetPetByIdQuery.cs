namespace Application.Modules.Pets.Queries;

public class GetPetByIdQuery(int id) : IRequest<PetDto>
{
    public int Id { get; set; } = id;
}
