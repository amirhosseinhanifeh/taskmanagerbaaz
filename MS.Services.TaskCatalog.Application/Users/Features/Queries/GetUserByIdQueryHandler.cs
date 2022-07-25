//using Ardalis.GuardClauses;
//using AutoMapper;
//using MS.Services.TaskCatalog.Contract.Users.Dtos;
//using MS.Services.TaskCatalog.Contract.Users.Request;
//using MS.Services.TaskCatalog.Contract.Users.Result;
//using MS.Services.TaskCatalog.Domain.Users;
//using MS.Services.TaskCatalog.Domain.Users.Exceptions.Application;
//using MS.Services.TaskCatalog.Infrastructure;
//using MsftFramework.Abstractions.CQRS.Query;
//using MsftFramework.Core.Exception;

//namespace MS.Services.TaskCatalog.Application.Users.Features.Queries;

//public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQueryRequest, GetUserByIdResult>
//{
//    private readonly ITaskCatalogDbContext taskCatalogDbContext;
//    private readonly IMapper mapper;

//    public GetUserByIdQueryHandler(ITaskCatalogDbContext taskCatalogDbContext, IMapper mapper)
//    {
//        this.taskCatalogDbContext = taskCatalogDbContext;
//        this.mapper = mapper;
//    }
//    public async Task<FluentResults.Result<GetUserByIdResult>> Handle(GetUserByIdQueryRequest query, CancellationToken cancellationToken)
//    {
//        Guard.Against.Null(query, nameof(query));
        
//        var user = await taskCatalogDbContext.FindUserByIdAsync(query.Id);
//        Guard.Against.Null(user, new UserNotFoundException(query.Id));

//        var userDto = mapper.Map<UserDto>(user);

//        var result = new FluentResults.Result();
//        return result.ToResult(new GetUserByIdResult(userDto));
//    }
//}