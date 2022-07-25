using MS.Services.TaskCatalog.Domain.Comments.Features.CreatingComment.Events.Domain;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Comments.Features.CreatingComment.Events.Notification;

public record CommentCreatedNotification(CommentCreatedEvent DomainEvent) : DomainNotificationEventWrapper<CommentCreatedEvent>(DomainEvent);