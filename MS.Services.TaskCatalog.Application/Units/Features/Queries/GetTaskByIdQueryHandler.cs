using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Units.Features.Queries;

public class GetTaskByIdQueryHandler :
    IQueryHandler<GetTaskByIdQueryRequest, GetTaskByIdResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetTaskByIdQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetTaskByIdResult>> Handle(GetTaskByIdQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));
        
        var Task = await taskCatalogDbContext.FindTaskByIdAsync(query.Id);
        Guard.Against.Null(Task, new TaskNotFoundException(query.Id));

        var TaskDto = mapper.Map<TaskDto>(Task);

        var result = new FluentResults.Result();
        return result.ToResult(new GetTaskByIdResult(TaskDto));
    }
}