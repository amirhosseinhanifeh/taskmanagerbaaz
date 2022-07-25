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

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Handlers;

public class CreateWorkflowStepsCommandHandler : ICommandHandler<CreateWorkflowStepsCommand, CreateWorkflowStepsResult>
{
    private readonly ILogger<CreateWorkflowStepsCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    public CreateWorkflowStepsCommandHandler(ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateWorkflowStepsCommandHandler> logger,
        ICommandProcessor commandProcessor)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }

    public async Task<Result<CreateWorkflowStepsResult>> Handle(CreateWorkflowStepsCommand request, CancellationToken cancellationToken)
    {
        var workflowSteps = Domain.Workflows.WorkflowSteps.Create(request.Id, request.name,request.workflowId,request.Deadline
            );
        var result = new Result();
        await _taskCatalogDbContext.WorkflowSteps.AddAsync(workflowSteps);
        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);
        var created = await _taskCatalogDbContext.WorkflowSteps
                .FirstOrDefaultAsync(x => x.Id == workflowSteps.Id, cancellationToken);
        var TaskDto = _mapper.Map<WorkflowStepsDto>(created);
        return result.ToResult(new CreateWorkflowStepsResult(TaskDto));
    }
}
