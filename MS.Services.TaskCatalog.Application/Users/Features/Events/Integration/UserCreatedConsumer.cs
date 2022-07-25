using Ardalis.GuardClauses;
using MS.Services.UserManagement.Contract.Users.Events.Domain;
using MS.Services.UserManagement.Contract.Users.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Users.Features.Events.Integration;
public class ProductCreatedConsumer :
   IIntegrationEventHandler<UserCreatedIEvent>,
   IIntegrationEventHandler<IntegrationEventWrapper<UserCreatedEvent>>
{
    public Task Handle(UserCreatedIEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        return Task.CompletedTask;
    }

    public Task Handle(IntegrationEventWrapper<UserCreatedEvent> notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        return Task.CompletedTask;
    }
}