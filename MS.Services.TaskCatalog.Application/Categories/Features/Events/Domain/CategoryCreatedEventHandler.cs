using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Categories.Features.Events.Domain;

internal class CategoryCreatedEventHandler : IDomainEventHandler<TaskCreatedEvent>
{
    //private readonly TaskCatalogDbContext _dbContext;

    //public CategoryCreatedEventHandler(TaskCatalogDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));

        //Handle Domain Event
        //for example update view models

        return Task.CompletedTask;
    }
}