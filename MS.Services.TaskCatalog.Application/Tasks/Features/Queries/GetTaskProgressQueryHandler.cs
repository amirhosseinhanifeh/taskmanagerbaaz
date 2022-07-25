using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Queries;


public class GetTaskProgressQueryHandler :
    IQueryHandler<GetTaskProgressQueryRequest, GetTaskProgressResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetTaskProgressQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetTaskProgressResult>> Handle(GetTaskProgressQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var TaskProgress = await taskCatalogDbContext.GetProgressesByTaskIdAsync(query.id);

        var TaskProgressDto = mapper.Map<List<TaskProgressDto>>(TaskProgress);

        var result = new FluentResults.Result();
        return result.ToResult(new GetTaskProgressResult(TaskProgressDto));
    }
}