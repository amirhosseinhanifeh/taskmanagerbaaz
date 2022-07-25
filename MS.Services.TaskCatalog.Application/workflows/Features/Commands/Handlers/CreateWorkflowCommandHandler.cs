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
using Microsoft.AspNetCore.Http;
using MS.Services.TaskCatalog.Domain.Resources;
using MediatR;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Handlers;

public class CreateWorkflowCommandHandler : ICommandHandler<CreateWorkflowCommand,Unit>
{
    private readonly ILogger<CreateWorkflowCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    public CreateWorkflowCommandHandler(ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateWorkflowCommandHandler> logger,
        ICommandProcessor commandProcessor)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }

    public async Task<Result<Unit>> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = Workflow.Create(request.Id, request.name);

        foreach (var step in request.WorkflowStepDto)
            workflow.CreateStep(step);

        await _taskCatalogDbContext.Workflows.AddAsync(workflow);
        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);

        var result = new Result();
        result = result.WithSuccess(new Success(Messages.Successful));
        return result;
    }
}
