namespace Application.Modules.Comments.Commands;

public sealed class DeleteCommentCommandValidator
    : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(query => query.CommentId)
            .NotEmpty();
    }
}
