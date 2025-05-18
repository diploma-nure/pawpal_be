namespace Application.Modules.Comments.Commands;

public sealed class AddCommentCommandValidator
    : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(command => command.Comment)
            .NotEmpty();

        RuleFor(command => command.PetId)
            .NotEmpty()
            .When(command => command.PetId is not null);
    }
}
