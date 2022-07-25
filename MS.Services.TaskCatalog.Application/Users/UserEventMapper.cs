
using MS.Services.UserManagement.Contract.Users.Events.Domain;
using MS.Services.UserManagement.Contract.Users.Events.Integration;
using MS.Services.UserManagement.Contract.Users.Events.Notification;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Users;
public class UserEventMapper : IEventMapper
{
    public UserEventMapper()
    {

    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        //return domainEvent switch
        //{
        //    UserCreatedEvent e =>
        //        new UserCreatedIEvent(
        //            e.id,
        //            e.name,
        //            ),
        //    //UserStockDebited e => new
        //    //    Features.DebitingProductStock.Events.Integration.UserStockDebited(
        //    //        e.UserId, e.NewStock.Available, e.DebitedQuantity),

        //    _ => null
        //};
        return null;
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            UserCreatedEvent e => new UserCreatedNotification(e),
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