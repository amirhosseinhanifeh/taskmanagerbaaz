using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Events.Domain;

internal class WorkflowCreatedEventHandler : IDomainEventHandler<TaskCreatedEvent>
{
    private readonly TaskCatalogDbContext _dbContext;

    public WorkflowCreatedEventHandler(TaskCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));

        //Handle Domain Event
        //for example update view models

        return Task.CompletedTask;
    }
}