namespace Application.Modules.Comments.Commands;

public class DeleteCommentCommand(int commentId) : IRequest<int>
{
    public int CommentId { get; set; } = commentId;
}
