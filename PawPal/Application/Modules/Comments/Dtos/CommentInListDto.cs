namespace Application.Modules.Comments.Dtos;

public class CommentInListDto
{
    public int Id { get; set; }

    public string Comment { get; set; }

    public UserShortDto User { get; set; }
}
