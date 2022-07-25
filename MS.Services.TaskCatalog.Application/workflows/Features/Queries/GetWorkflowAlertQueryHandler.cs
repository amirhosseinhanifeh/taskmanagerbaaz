using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Contract.workflows.Dtos;
using MS.Services.TaskCatalog.Contract.workflows.Result;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Queries;

public class GetWorkflowAlertQueryHandler : IQueryHandler<GetWorkflowAlertQueryRequest, GetWorkflowAlertsResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetWorkflowAlertQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetWorkflowAlertsResult>> Handle(GetWorkflowAlertQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var WorkflowAlerts = await taskCatalogDbContext.WorkFlowAlerts.ToListAsync();
        Guard.Against.Null(WorkflowAlerts, new WorkflowsNotFoundException());

        var GetWorkflowAlertDtos = mapper.Map<List<WorkflowAlertDto>>(WorkflowAlerts);
        var result = new FluentResults.Result();
        return result.ToResult(new GetWorkflowAlertsResult(GetWorkflowAlertDtos));
    }
}


