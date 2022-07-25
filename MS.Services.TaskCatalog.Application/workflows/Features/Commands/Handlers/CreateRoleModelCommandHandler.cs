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

public class CreateRoleModelCommandHandler : ICommandHandler<CreatetWorkflowRoleModelCommand, CreateRoleModelResult>
{
    private readonly ILogger<CreateWorkflowCommandHandler> _logger;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    private readonly IMapper _mapper;
    public CreateRoleModelCommandHandler(ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateWorkflowCommandHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
        _mapper = mapper;
    }

    public async Task<Result<CreateRoleModelResult>> Handle(CreatetWorkflowRoleModelCommand request, CancellationToken cancellationToken)
    {
        var workflowRoleModel = WorkflowRoleModel.Create(request.Id, request.Name, request.UnitId);

        foreach (var item in request.Roles)
            workflowRoleModel.Roles.Add(Role.Create(item.Name, item.RoleId, SnowFlakIdGenerator.NewId()));

        await _taskCatalogDbContext.WorkFlowRoleModels.AddAsync(workflowRoleModel);
        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);


        var data = await _taskCatalogDbContext.WorkFlowRoleModels
            .Include(x => x.Roles)
            .SingleOrDefaultAsync(x => x.UnitId == workflowRoleModel.Id, cancellationToken);

        var RoleDto = _mapper.Map<RoleModelDto>(data);
        var result = new Result();

        return result.ToResult(new CreateRoleModelResult(RoleDto));
    }
}
