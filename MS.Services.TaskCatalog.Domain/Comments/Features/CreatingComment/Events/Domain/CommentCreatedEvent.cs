using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Comments.Features.CreatingComment.Events.Domain;
public record CommentCreatedEvent(Comment Comment) : DomainEvent;