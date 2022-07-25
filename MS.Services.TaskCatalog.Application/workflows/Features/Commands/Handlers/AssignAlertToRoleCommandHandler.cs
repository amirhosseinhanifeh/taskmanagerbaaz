using MsftFramework.Abstractions.CQRS.Command;
using Microsoft.Extensions.Logging;
using MsftFramework.Core.Exception;
using AutoMapper;
using MS.Services.TaskCatalog.Infrastructure;
using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using FluentResults;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MS.Services.TaskCatalog.Domain.Workflows;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Domain.workflows;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Handlers;

public class AssignAlertToRoleCommandHandler : ICommandHandler<AssignAlertToRoleCommand, bool>
{
    private readonly ILogger<AssignAlertToRoleCommandHandler> _logger;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    public AssignAlertToRoleCommandHandler(ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<AssignAlertToRoleCommandHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }

    public async Task<Result<bool>> Handle(AssignAlertToRoleCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.model);

        //foreach (var item in request.model)
            //await _taskCatalogDbContext.WorkflowStepAlertInstances.AddRangeAsync(WorkflowStepAlertInstance.Create(SnowFlakIdGenerator.NewId(), item.Order, item.Delay, item.WorkflowAlertId, request.RoleId));

        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);

        return new Result().ToResult(true);
    }
}
