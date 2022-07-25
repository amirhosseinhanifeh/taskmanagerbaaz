using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Notification;

public record WorkflowCreatedNotification(WorkflowCreatedEvent DomainEvent) : DomainNotificationEventWrapper<WorkflowCreatedEvent>(DomainEvent);

