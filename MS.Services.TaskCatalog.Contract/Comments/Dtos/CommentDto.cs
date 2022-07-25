using Newtonsoft.Json;

namespace MS.Services.TaskCatalog.Contract.Comments.Dtos;
public record CommentDto
{
    public long Id { get; init; }
    public string? Body { get; private set; }
    public long? CommentId { get; private set; }
    public ICollection<CommentDto>? Comments { get; private set; }
}