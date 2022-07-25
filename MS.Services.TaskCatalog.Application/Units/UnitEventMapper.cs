using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Integration;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Notification;
using MS.Services.UserManagement.Contract.Units.Events.Domain;
using MS.Services.UserManagement.Contract.Units.Events.Integration;
using MS.Services.UserManagement.Contract.Units.Events.Notification;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Units;
public class UnitEventMapper : IEventMapper
{
    public UnitEventMapper()
    {

    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            UnitCreatedEvent e =>
                new UnitCreatedIEvent(
                    e.id,
                    e.name),
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
            UnitCreatedEvent e => new UnitCreatedNotification(e),
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