
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Integration;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProjects.Events.Domain;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;
using MsftFramework.Abstractions.Messaging.Outbox;

namespace MS.Services.TaskCatalog.Application.Projects.Features.Events.Domain;

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class ProjectCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<ProjectCreatedEvent>
{
    private readonly IOutboxService _outboxService;

    public ProjectCreatedDomainEventToIntegrationMappingHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public Task Handle(ProjectCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        ProjectCreatedIEvent projectCreatedIEvent = new ProjectCreatedIEvent(domainEvent.Project.Id, domainEvent.Project.Name);
        _outboxService.SaveAsync(projectCreatedIEvent,cancellationToken);
        return Task.CompletedTask;
    }
}

