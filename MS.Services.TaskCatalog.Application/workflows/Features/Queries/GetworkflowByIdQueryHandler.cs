using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Queries;

public class GetWorkflowByIdQueryHandler :IQueryHandler<GetWorkflowByIdQueryRequest, GetWorkflowByIdResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetWorkflowByIdQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetWorkflowByIdResult>> Handle(GetWorkflowByIdQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var Workflow = await taskCatalogDbContext.GetWorkflowByIdAsync(query.Id);
        Guard.Against.Null(Workflow, new WorkflowsByIdNotFoundException(query.Id));

        //var WorkflowDto = mapper.Map<List<WorkflowManagerDto>>(Workflow);

        var GetWorkflowDtoMap = mapper.Map<WorkflowDto>(Workflow);


        var result = new FluentResults.Result();
        return result.ToResult(new GetWorkflowByIdResult(GetWorkflowDtoMap));
    }
}
