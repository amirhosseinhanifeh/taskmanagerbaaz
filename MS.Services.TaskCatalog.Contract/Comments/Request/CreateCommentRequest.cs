namespace MS.Services.TaskCatalog.Contract.Comments.Request;
public record CreateCommentRequest
{
    public string? Body { get; init; }
    public long? CommentId { get; set; } = null;
}