using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Integration;
using MS.Services.UserManagement.Contract.Units.Events.Domain;
using MS.Services.UserManagement.Contract.Units.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;
using MsftFramework.Abstractions.Messaging.Outbox;

namespace MS.Services.TaskCatalog.Application.Units.Features.Events.Domain;

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class UnitCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<UnitCreatedEvent>
{
    private readonly IOutboxService _outboxService;

    public UnitCreatedDomainEventToIntegrationMappingHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public Task Handle(UnitCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        UnitCreatedIEvent TaskCreatedIEvent = new UnitCreatedIEvent(domainEvent.id, domainEvent.name);
        _outboxService.SaveAsync(TaskCreatedIEvent, cancellationToken);
        return Task.CompletedTask;
    }
}

