using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Integration;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Notification;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Categories;
public class CategoryEventMapper : IEventMapper
{
    public CategoryEventMapper()
    {

    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            TaskCreatedEvent e =>
                new TaskCreatedIEvent(
                    e.Task.Id,
                    e.Task.Name),
            //TaskStockDebited e => new
            //    Features.DebitingTaskStock.Events.Integration.TaskStockDebited(
            //        e.TaskId, e.NewStock.Available, e.DebitedQuantity),
            
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            TaskCreatedEvent e => new TaskCreatedNotification(e),
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