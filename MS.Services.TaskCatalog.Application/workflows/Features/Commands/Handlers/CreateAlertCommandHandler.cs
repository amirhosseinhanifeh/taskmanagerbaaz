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

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Handlers;

public class CreateAlertCommandHandler : ICommandHandler<CreateAlertCommand, bool>
{
    private readonly ILogger<CreateWorkflowCommandHandler> _logger;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    public CreateAlertCommandHandler(ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateWorkflowCommandHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }

    public async Task<Result<bool>> Handle(CreateAlertCommand request, CancellationToken cancellationToken)
    {
        var workflowAlert = WorkFlowAlerts.Create(request.Id,request.body);

     
        await _taskCatalogDbContext.WorkFlowAlerts.AddAsync(workflowAlert);
        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);

        var result = new Result();
        return result.ToResult(true);
    }
}
