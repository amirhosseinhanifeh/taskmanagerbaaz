using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Application.Hubs;
using MS.Services.TaskCatalog.Contract.Projects.Dtos;
using MS.Services.TaskCatalog.Contract.Projects.Request;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Projects.Features.Queries;


public class GetProjectsQueryHandler :
    IQueryHandler<GetProjectsQueryRequest, GetProjectResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetProjectsQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetProjectResult>> Handle(GetProjectsQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        long? userId = null;
        if (query.hasSelection == true)
            userId = 1;

        var Task = await taskCatalogDbContext.GetProjectsAsync(userId, cancellationToken);

        var TaskDto = mapper.Map<List<ProjectDto>>(Task);


        var w = await taskCatalogDbContext.WorkflowInstance.Include(x=>x.workflowSteps).FirstOrDefaultAsync(cancellationToken);
        var WorkflowDto = mapper.Map<WorkflowInstanceDto>(w);
       await WorkflowInstanceHub.UpdateWorkflowInstanceById(WorkflowDto);

        var result = new FluentResults.Result();
        return result.ToResult(new GetProjectResult(TaskDto));
    }
}