using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract.Users.Dtos;
using MS.Services.TaskCatalog.Contract.Users.Request;
using MS.Services.TaskCatalog.Contract.Users.Result;
using MS.Services.TaskCatalog.Domain.Users.Exceptions.Application;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Users.Features.Queries;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQueryRequest, GetUsersResult>
{
    private readonly ITaskCatalogDbContext taskCatalogDbContext;
    private readonly IMapper mapper;

    public GetUsersQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
    {
        this.taskCatalogDbContext = taskCatalogDbContext;
        this.mapper = mapper;
    }
    public async Task<FluentResults.Result<GetUsersResult>> Handle(GetUsersQueryRequest query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        long? userId = null;
        if (query.hasSelection==true)
            userId = 1;
        var users = await taskCatalogDbContext.GetUsersAsync(userId,cancellationToken);


        var userDto = mapper.Map<IList<UserDto>>(users);

        var result = new FluentResults.Result();
        return result.ToResult(new GetUsersResult(userDto));
    }
}