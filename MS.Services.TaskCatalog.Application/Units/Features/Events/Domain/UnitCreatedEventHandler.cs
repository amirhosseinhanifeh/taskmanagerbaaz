using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Infrastructure;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MS.Services.UserManagement.Contract.Units.Events.Integration;
using MsftFramework.Abstractions.Core.Domain.Events.External;
using System.Data.Common;

namespace MS.Services.TaskCatalog.Application.Units.Features.Events.Domain;

internal class UnitCreatedEventHandler : IIntegrationEventHandler<UnitCreatedIEvent>
{
    private readonly TaskCatalogDbContext _dbContext;

    public UnitCreatedEventHandler(TaskCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(UnitCreatedIEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));

        var unit = MS.Services.TaskCatalog.Domain.Tasks.Unit.Create(notification.UnitId, notification.UnitName);

        //Task.Run(() =>
        //{
        //    if (!_dbContext.Units.Any(x => x.Id == unit.Id))
        //    {
        //        _dbContext.Units.AddAsync(unit, cancellationToken);
        //        _dbContext.SaveChangesAsync(cancellationToken);
        //    }

        //});
        return Task.CompletedTask;
    }
}