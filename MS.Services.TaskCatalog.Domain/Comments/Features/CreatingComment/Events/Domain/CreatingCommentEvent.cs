using MS.Services.TaskCatalog.Domain.Comments.ValueObjects;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Comments.Features.CreatingComment.Events.Domain;
public record CreatingCommentEvent(
    CommentId Id,
    CommentName Name,
    string? Description = null) : DomainEvent;