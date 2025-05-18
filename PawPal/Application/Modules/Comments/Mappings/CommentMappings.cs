namespace Application.Modules.Comments.Mappings;

public static class CommentMappings
{
    public static CommentInListDto ToCommentInListDto(this Comment comment)
        => new()
        {
            Id = comment.Id,
            Comment = comment.Value,
            User = comment.User.ToUserShortDto(),
        };
}