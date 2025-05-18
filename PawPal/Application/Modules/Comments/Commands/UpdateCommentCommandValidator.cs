namespace Application.Modules.Comments.Commands;

public sealed class UpdateCommentCommandValidator
    : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(command => command.Id)
           .NotEmpty();
    }
}
