namespace Application.Modules.Pets.Queries;

public class GetPetByIdQuery(int petId) : IRequest<PetDto>
{
    public int PetId { get; set; } = petId;
}
