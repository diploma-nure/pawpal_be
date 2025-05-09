namespace Application.Modules.Pets.Queries;

public sealed class GetPetByIdQueryValidator
    : AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdQueryValidator()
    {
        RuleFor(query => query.PetId)
            .NotEmpty();
    }
}
