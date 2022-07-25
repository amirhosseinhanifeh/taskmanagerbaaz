using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Queries;

public class GetWorkflowInstanceQueryHandler : IQueryHandler<GetWorkflowInstanceQueryRequest, GetWorkflowInstanceResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetWorkflowInstanceQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetWorkflowInstanceResult>> Handle(GetWorkflowInstanceQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var Workflow = taskCatalogDbContext.WorkflowInstance.Include(x => x.workflowSteps)
            .AsQueryable();

        if (query.workflowId != null)
            Workflow = Workflow.Where(x => x.WorkflowId == query.workflowId);

        var res = await Workflow.ToListAsync(cancellationToken);
        Guard.Against.Null(Workflow, new WorkflowsNotFoundException());

        //var WorkflowDto = mapper.Map<List<WorkflowManagerDto>>(Workflow);

        var WorkflowDto = mapper.Map<List<WorkflowInstanceDto>>(res);
        var result = new FluentResults.Result();
        return result.ToResult(new GetWorkflowInstanceResult(WorkflowDto));
    }
}


