using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using MediatR;
using MS.Services.TaskCatalog.Contract.Categories.Dtos;
using MS.Services.TaskCatalog.Contract.Categories.Request;
using MS.Services.TaskCatalog.Contract.Categories.Result;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Categories.Features.Queries;

public class GetCategoryByIdQueryHandler :
    IQueryHandler<GetCategoryByIdQueryRequest, GetCategoryByIdResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetCategoryByIdQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<Result<GetCategoryByIdResult>> Handle(GetCategoryByIdQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));
        
        var Task = await taskCatalogDbContext.FindTaskByIdAsync(query.Id);
        Guard.Against.Null(Task, new TaskNotFoundException(query.Id));

        var TaskDto = mapper.Map<CategoryDto>(Task);

        var result = new Result();
        return result.ToResult(new GetCategoryByIdResult(TaskDto));
    }

   
}