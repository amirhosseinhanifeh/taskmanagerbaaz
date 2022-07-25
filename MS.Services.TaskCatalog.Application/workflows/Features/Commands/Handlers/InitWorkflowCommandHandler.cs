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
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Handlers;

public class InitWorkflowCommandHandler : ICommandHandler<InitWorkflowCommand, InitWorkflowResult>
{
    private readonly ILogger<InitWorkflowCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly TaskCatalogDbContext _taskCatalogDbContext;
    public InitWorkflowCommandHandler(TaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<InitWorkflowCommandHandler> logger,
        ICommandProcessor commandProcessor)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }

    public async Task<Result<InitWorkflowResult>> Handle(InitWorkflowCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.Id);

        var workflow = await _taskCatalogDbContext.Workflows
              .Include(e => e.WorkflowSteps)
              .ThenInclude(c => c.WorkflowRoleModel)
              .ThenInclude(x => x.Roles)
              .Include(x => x.WorkflowSteps)
              .FirstOrDefaultAsync(e => e.Id == request.workflowId);

        var workflowInstanceModel = await _taskCatalogDbContext.WorkflowInstance
            .FirstOrDefaultAsync(x => x.Id == request.workflowInstanceId);

        var rr = request.roles.Select(e => new WorkflowUserRole
        {
            RoleId = e.RoleId,
            UserId = e.UserId,
            WorkflowStepId = e.WorkflowStepId,
            Alerts = e.Alerts.Select(h => new AssingAlertToRole
            {
                AlertId = h.AlertId,
                Delay = h.Delay,
                Order = h.Order,

            }).ToList()
        }).ToList();

        var workflowInstance = WorkflowInstance.Create(workflow, workflowInstanceModel, request.name, request.description, request.workflowstepId, rr);

        if (workflowInstanceModel != null)
        {
            foreach (var item in workflowInstance.workflowSteps)
            {
                await _taskCatalogDbContext.WorkflowStepInstances.AddAsync(item);
                await _taskCatalogDbContext.SaveChangesAsync();

            }
        }

        if (workflowInstance != null) //when stata is null item return null
        {
            try
            {
                if (workflowInstanceModel == null)
                {
                    await _taskCatalogDbContext.WorkflowInstance.AddAsync(workflowInstance);
                    await _taskCatalogDbContext.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        var created = await _taskCatalogDbContext.WorkflowInstance
                .FirstOrDefaultAsync(x => x.Id == workflowInstance.Id, cancellationToken);
        var TaskDto = _mapper.Map<WorkflowInstanceDto>(created);
        var result = new Result();
        return result.ToResult(new InitWorkflowResult(TaskDto));
    }
}
