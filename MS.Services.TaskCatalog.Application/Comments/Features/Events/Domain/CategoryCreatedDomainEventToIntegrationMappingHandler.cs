using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;
using MsftFramework.Abstractions.Messaging.Outbox;

namespace MS.Services.TaskCatalog.Application.Comments.Features.Events.Domain;

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class CategoryCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<TaskCreatedEvent>
{
    private readonly IOutboxService _outboxService;

    //public CategoryCreatedDomainEventToIntegrationMappingHandler(IOutboxService outboxService)
    //{
    //    _outboxService = outboxService;
    //}

    public Task Handle(TaskCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        //TaskCreatedIEvent TaskCreatedIEvent = new TaskCreatedIEvent(domainEvent.Task.Id, domainEvent.Task.Name);
        //_outboxService.SaveAsync(TaskCreatedIEvent,cancellationToken);
        return Task.CompletedTask;
    }
}

