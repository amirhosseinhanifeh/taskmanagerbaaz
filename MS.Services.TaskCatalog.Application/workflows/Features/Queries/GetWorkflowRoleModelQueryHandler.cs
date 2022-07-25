using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Queries;

public class GetWorkflowRoleModelQueryHandler : IQueryHandler<GetWorkflowRoleModelRequest, GetWorkflowRoleModelResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetWorkflowRoleModelQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetWorkflowRoleModelResult>> Handle(GetWorkflowRoleModelRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var res = taskCatalogDbContext.WorkFlowRoleModels
            .Include(x=>x.Roles)
            .AsQueryable();

        if (query.UnitId != null)
            res = res.Where(x => x.UnitId == query.UnitId);

        var data = await res.ToListAsync();

        var rolemodelDtos = mapper.Map<List<RoleModelDto>>(data);

        var result = new FluentResults.Result();
        return result.ToResult(new GetWorkflowRoleModelResult(rolemodelDtos));
    }
}


