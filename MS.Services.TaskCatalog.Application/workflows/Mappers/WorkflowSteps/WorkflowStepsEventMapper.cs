using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Integration;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Notification;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Application.workflows.Mappers.WorkflowSteps
{
    public class WorkflowStepsEventMapper : IEventMapper
    {
        public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
        {
            return domainEvent switch
            {
                WorkflowStepsCreatedEvent e => new WorkflowStepsCreatedNotification(e),
                _ => null
            };
        }

        public IReadOnlyList<IDomainNotificationEvent?>? MapToDomainNotificationEvents(IReadOnlyList<IDomainEvent> domainEvents)
        {
            return domainEvents.Select(MapToDomainNotificationEvent).ToList().AsReadOnly();
        }

        public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
        {
            return domainEvent switch
            {
                CreatingWorkflowManagerEvent e =>
                    new WorkflowStepsIEvent(
                         e.id,
                         e.name
                         ),
                //WorkflowstockDebited e => new
                //    Features.DebitingProductStock.Events.Integration.WorkflowstockDebited(
                //        e.WorkflowId, e.NewStock.Available, e.DebitedQuantity),

                _ => null
            };
        }

        public IReadOnlyList<IIntegrationEvent?>? MapToIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents)
        {
            return domainEvents.Select(MapToIntegrationEvent).ToList().AsReadOnly();
        }
    }
}
