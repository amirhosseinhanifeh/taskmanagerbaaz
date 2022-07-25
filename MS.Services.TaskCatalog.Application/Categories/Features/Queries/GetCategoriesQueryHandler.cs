using Ardalis.GuardClauses;
using AutoMapper;
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


public class GetCategoriesQueryHandler :
    IQueryHandler<GetCategoriesQueryRequest, GetCategoriesResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetCategoriesQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetCategoriesResult>> Handle(GetCategoriesQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));
        
        var Task = await taskCatalogDbContext.GetCategoriesAsync();

        var TaskDto = mapper.Map<List<CategoryDto>>(Task);

        var result = new FluentResults.Result();
        return result.ToResult(new GetCategoriesResult(TaskDto));
    }
}