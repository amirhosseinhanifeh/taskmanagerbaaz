   using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Notification;

public record TaskCreatedNotification(TaskCreatedEvent DomainEvent) : DomainNotificationEventWrapper<TaskCreatedEvent>(DomainEvent);