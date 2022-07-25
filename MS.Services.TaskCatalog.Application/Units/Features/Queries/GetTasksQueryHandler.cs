using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Units.Features.Queries;


//public class GetUnitsQueryHandler :
//    IQueryHandler<GetUnitsQueryRequest, GetTasksResult>
//{
//    private readonly ITaskCatalogDbContext taskCatalogDbContext;
//    private readonly IMapper mapper;

//    public GetUnitsQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
//    {
//        this.taskCatalogDbContext = taskCatalogDbContext;
//        this.mapper = mapper;
//    }
//    public async Task<FluentResults.Result<GetTasksResult>> Handle(GetTasksQueryRequest query, CancellationToken cancellationToken)
//    {
//        Guard.Against.Null(query, nameof(query));

//        var Task = await taskCatalogDbContext.GetTasksAsync(query.name, query.projectId, query.unitId, query.userId, query.controllerId, query.testerId, query.creatoruserId, query.startDate, query.endDate, query.priorityType, query.pageSize, query.page, query.sort, cancellationToken);

//        var TaskDto = mapper.Map<List<TasksDto>>(Task);

//        var result = new FluentResults.Result();
//        return result.ToResult(new GetTasksResult(TaskDto,null));
//    }
//}