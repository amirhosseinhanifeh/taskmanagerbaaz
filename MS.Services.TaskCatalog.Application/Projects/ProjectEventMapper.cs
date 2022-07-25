using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Domain;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Integration;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Notification;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProjects.Events.Domain;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Projects;
public class ProjectEventMapper : IEventMapper
{
    public ProjectEventMapper()
    {

    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ProjectCreatedEvent e =>
                new ProjectCreatedIEvent(
                    e.Project.Id,
                    e.Project.Name),
            //ProjectStockDebited e => new
            //    Features.DebitingProductStock.Events.Integration.ProjectStockDebited(
            //        e.ProjectId, e.NewStock.Available, e.DebitedQuantity),
            
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ProjectCreatedEvent e => new ProjectCreatedNotification(e),
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