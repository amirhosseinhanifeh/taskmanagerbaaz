using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Integration;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProjects.Events.Domain;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Projects.Features.Events.Domain;

internal class ProjectCreatedEventHandler : IDomainEventHandler<ProjectCreatedEvent>
{
    private readonly TaskCatalogDbContext _dbContext;

    public ProjectCreatedEventHandler(TaskCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(ProjectCreatedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));

        //Handle Domain Event
        //for example update view models

        return Task.CompletedTask;
    }
}