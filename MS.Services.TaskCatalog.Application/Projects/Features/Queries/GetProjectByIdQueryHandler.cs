using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using MS.Services.TaskCatalog.Contract.Projects.Dtos;
using MS.Services.TaskCatalog.Contract.Projects.Request;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Domain.Projects.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Projects.Features.Queries;

public class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQueryRequest, GetProjectByIdResult>
{
    private readonly ITaskCatalogDbContext TaskCatalogDbContext;
    private readonly IMapper mapper;

    public GetProjectByIdQueryHandler(ITaskCatalogDbContext TaskCatalogDbContext, IMapper mapper)
    {
        this.TaskCatalogDbContext = TaskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<Result<GetProjectByIdResult>> Handle(GetProjectByIdQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));
        
        var project = await TaskCatalogDbContext.FindProjectByIdAsync(query.Id);
        Guard.Against.Null(project, new ProjectNotFoundException(query.Id));

        var projectDto = mapper.Map<ProjectDto>(project);

        var result = new Result();
        return result.ToResult(new GetProjectByIdResult(projectDto));
    }
}