using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Workflows;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;
using MsftFramework.Abstractions.Messaging.Outbox;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Events.Domain;

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class TaskCompletedEventHandler : IDomainEventHandler<TaskDoneEvent>
{

    public TaskCompletedEventHandler()
    {
    }

    public Task Handle(TaskDoneEvent domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        WorkflowManager.Manager.DoneTask(domainEvent.Task);

        return Task.CompletedTask;
    }
}

