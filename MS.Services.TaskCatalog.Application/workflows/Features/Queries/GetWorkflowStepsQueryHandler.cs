//using Ardalis.GuardClauses;
//using AutoMapper;
//using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
//using MS.Services.TaskCatalog.Contract.Workflows.Request;
//using MS.Services.TaskCatalog.Contract.Workflows.Result;
//using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;
//using MS.Services.TaskCatalog.Infrastructure;
//using MsftFramework.Abstractions.CQRS.Query;
//using MsftFramework.Core.Exception;

//namespace MS.Services.TaskCatalog.Application.Workflows.Features.Queries;

//public class GetWorkflowStepsQueryHandler : IQueryHandler<GetWorkflowStepsQueryRequest, GetWorkflowStepsResult>
//{
//    private readonly ITaskCatalogDbContext taskCatalogDbContext;
//    private readonly IMapper mapper;

//    public GetWorkflowStepsQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
//    {
//        this.taskCatalogDbContext = taskCatalogDbContext;
//        this.mapper = mapper;
//    }
//    public async Task<FluentResults.Result<GetWorkflowStepsResult>> Handle(GetWorkflowStepsQueryRequest query, CancellationToken cancellationToken)
//    {
//        Guard.Against.Null(query, nameof(query));

//        var workflowSteps = await taskCatalogDbContext.GetAllWorkflowStepsAsync();
//        Guard.Against.Null(workflowSteps, new GetWorkflowStepNotFoundException());

//        //var WorkflowDto = mapper.Map<List<WorkflowManagerDto>>(Workflow);

//        var GetWokflowStepsDtoMap = mapper.Map<List<WorkflowStepsDto>>(workflowSteps);
//        var result = new FluentResults.Result();
//        return result.ToResult(new GetWorkflowStepsResult(GetWokflowStepsDtoMap));
//    }
//}


