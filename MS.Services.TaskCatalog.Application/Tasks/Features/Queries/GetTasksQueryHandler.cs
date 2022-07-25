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


public class GetTasksQueryHandler :
    IQueryHandler<GetTasksQueryRequest, GetTasksResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetTasksQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<Result<GetTasksResult>> Handle(GetTasksQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var Tasks = taskCatalogDbContext.GetTasks(
            query.name, 
            query.projectIds, 
            query.unitIds, 
            query.userIds, 
            query.controllerId,
            query.testerId,
            query.creatoruserId,
            false,
            query.startDate,
            query.endDate, 
            query.priorityType,
            query.sort,
            null)
            .Where(x => x.Status != Domain.SharedKernel.TaskStatus.Done);
        
        var list = await Tasks.Skip((query.page - 1) * query.pageSize).Take(query.pageSize).ToListAsync();
        var TaskDto = mapper.Map<List<TasksDto>>(list);


        var TodayTasks = await taskCatalogDbContext.GetTodayTasksAsync(1, query.todayPriorityOrder).ToListAsync();

        var TaskTodayDto = mapper.Map<List<TasksDto>>(TodayTasks.Where(x =>x.IsTodayTask==true && x.StartDateTime.Date==DateTime.Now.Date));

        var DoneTasksDto = mapper.Map<List<TasksDto>>(Tasks.Where(x => x.Status == Domain.SharedKernel.TaskStatus.Done));

        var UnDoneTasksDto = mapper.Map<List<TasksDto>>(Tasks.Where(x => x.StartDateTime.Date == DateTime.Now.Date && x.Status == Domain.SharedKernel.TaskStatus.UnDone));

        var UnCompleteTasksDto = mapper.Map<List<TasksDto>>(Tasks.Where(x => x.StartDateTime.Date == DateTime.Now.Date && x.IsTodayTask && x.Status == Domain.SharedKernel.TaskStatus.UnCompleted));

        var result = new Result();
        return result.ToResult(new GetTasksResult(TaskDto, TaskTodayDto, UnDoneTasksDto, DoneTasksDto, UnCompleteTasksDto, Tasks.Count(x=> x.Status != Domain.SharedKernel.TaskStatus.Done)));
    }
}