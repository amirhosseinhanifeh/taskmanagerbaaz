
using MS.Services.UserManagement.Contract.Users.Events.Domain;
using MS.Services.UserManagement.Contract.Users.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;
using MsftFramework.Abstractions.Messaging.Outbox;

namespace MS.Services.TaskCatalog.Application.Users.Features.Events.Domain;

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class UserCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly IOutboxService _outboxService;

    public UserCreatedDomainEventToIntegrationMappingHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public Task Handle(UserCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        UserCreatedIEvent userCreatedIEvent = new UserCreatedIEvent(domainEvent.id, domainEvent.name);
        _outboxService.SaveAsync(userCreatedIEvent, cancellationToken);
        return Task.CompletedTask;
    }
}

