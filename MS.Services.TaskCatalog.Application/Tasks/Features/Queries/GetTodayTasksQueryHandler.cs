using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Queries;


public class GetTodayTasksQueryHandler :
    IQueryHandler<GetTodayTasksQueryRequest, GetTodayTasksResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetTodayTasksQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<Result<GetTodayTasksResult>> Handle(GetTodayTasksQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var Tasks = taskCatalogDbContext.GetTasks(query.name, 
            query.projectIds, 
            query.unitIds, 
            query.userIds, 
            query.controllerId, 
            query.testerId, 
            query.creatoruserId,
            true,
            query.startDate, 
            query.endDate, 
            query.priorityType, 
            query.sort,
            query.todayPriorityOrder
            );
        var TodayTasks = await Tasks.Include(x=>x.Users).Skip((query.page - 1) * query.pageSize).Take(query.pageSize).ToListAsync();

        var TaskTodayDto = mapper.Map<List<TodayTasksDto>>(TodayTasks);

        var result = new Result();
        return result.ToResult(new GetTodayTasksResult(TaskTodayDto, Tasks.Count()));
    }
}