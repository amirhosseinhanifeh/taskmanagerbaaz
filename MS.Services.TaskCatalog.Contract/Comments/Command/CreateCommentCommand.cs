using MS.Services.TaskCatalog.Contract.Comments.Result;
using MS.Services.TaskCatalog.Contract.Comments.Request;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Comments.Commands;
public record CreateCommentCommand(
    string Body,
    long? CommentId
    ) : ITxCreateCommand<FluentResults.Result<bool>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
    public long TaskId { get; set; }
}