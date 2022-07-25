using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MS.Services.UserManagement.Contract.Users.Events.Domain;
using MS.Services.UserManagement.Contract.Users.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.External;

namespace MS.Services.TaskCatalog.Application.Users.Features.Events.Domain;

internal class UserCreatedEventHandler : IIntegrationEventHandler<UserCreatedIEvent>
{
    private readonly TaskCatalogDbContext _dbContext;

    public UserCreatedEventHandler(TaskCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(UserCreatedIEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));

        if (!_dbContext.Users.Any(h => h.Id == notification.Id))
        {
            var user = TaskCatalog.Domain.Tasks.User.Create(notification.Id,notification.UserName, null);
            _dbContext.Users.AddAsync(user, cancellationToken);
            _dbContext.SaveChangesAsync(cancellationToken);
        }
        //Handle Domain Event
        //for example update view models

        return Task.CompletedTask;
    }
}