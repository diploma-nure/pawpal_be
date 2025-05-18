namespace Application.Modules.Comments.Commands;

public class UpdateCommentCommand : IRequest<int>
{
    public int Id { get; set; }

    public string? Comment { get; set; }
}
