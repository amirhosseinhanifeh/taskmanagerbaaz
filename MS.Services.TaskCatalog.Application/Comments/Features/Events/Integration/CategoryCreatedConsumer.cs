using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Application.Comments.Features.Events.Integration;
public class CategoryCreatedConsumer :
   IIntegrationEventHandler<TaskCreatedIEvent>,
   IIntegrationEventHandler<IntegrationEventWrapper<TaskCreatedEvent>>
{
    public Task Handle(TaskCreatedIEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
       
        return Task.CompletedTask;
    }

    public Task Handle(IntegrationEventWrapper<TaskCreatedEvent> notification, CancellationToken cancellationToken)
    {
        
        Guard.Against.Null(notification, nameof(notification));
        return Task.CompletedTask;
    }
}