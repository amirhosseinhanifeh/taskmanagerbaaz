using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Integration;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Notification;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.workflows.Mappers.Workflow;
public class WorkflowEventMapper : IEventMapper
{
    public WorkflowEventMapper()
    {

    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            WorkflowCreatedEvent e =>
                new WorkflowCreatedIEvent(
                     e.Workflow.Id,
                     e.Workflow.Name
                     ),
            //WorkflowstockDebited e => new
            //    Features.DebitingProductStock.Events.Integration.WorkflowstockDebited(
            //        e.WorkflowId, e.NewStock.Available, e.DebitedQuantity),

            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            WorkflowCreatedEvent e => new WorkflowCreatedNotification(e),
            _ => null
        };
    }

    public IReadOnlyList<IIntegrationEvent?> MapToIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(MapToIntegrationEvent).ToList().AsReadOnly();
    }

    public IReadOnlyList<IDomainNotificationEvent?> MapToDomainNotificationEvents(
        IReadOnlyList<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(MapToDomainNotificationEvent).ToList().AsReadOnly();
    }
}
