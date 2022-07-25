using Ardalis.GuardClauses;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Core.Domain.Events.Internal;
using MS.Services.UserManagement.Contract.Units.Command;
using MS.Services.UserManagement.Contract.Units.Events.Integration;
using MS.Services.UserManagement.Contract.Units.Events.Domain;

namespace MS.Services.TaskCatalog.Application.Units.Features.Events.Integration;
public class UnitCreatedConsumer :
   IIntegrationEventHandler<UnitCreatedIEvent>,
   IIntegrationEventHandler<IntegrationEventWrapper<UnitCreatedEvent>>
{
    public Task Handle(UnitCreatedIEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));

        return Task.CompletedTask;
    }

    public Task Handle(IntegrationEventWrapper<UnitCreatedEvent> notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        return Task.CompletedTask;
    }
}