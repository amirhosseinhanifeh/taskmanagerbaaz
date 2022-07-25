using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Integration;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProjects.Events.Domain;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Projects.Features.Events.Integration;
public class ProductCreatedConsumer :
   IIntegrationEventHandler<ProjectCreatedIEvent>,
   IIntegrationEventHandler<IntegrationEventWrapper<ProjectCreatedEvent>>
{
    public Task Handle(ProjectCreatedIEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        return Task.CompletedTask;
    }

    public Task Handle(IntegrationEventWrapper<ProjectCreatedEvent> notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        return Task.CompletedTask;
    }
}