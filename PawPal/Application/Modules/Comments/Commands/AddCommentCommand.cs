namespace Application.Modules.Comments.Commands;

public class AddCommentCommand : IRequest<int>
{
    public string Comment { get; set; }

    public int? PetId { get; set; }
}
