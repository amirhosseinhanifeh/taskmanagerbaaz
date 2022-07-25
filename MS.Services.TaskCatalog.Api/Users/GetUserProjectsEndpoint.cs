//using Ardalis.GuardClauses;
//using MS.Services.TaskCatalog.Api.Tasks;
//using MS.Services.TaskCatalog.Contract;
//using MS.Services.TaskCatalog.Contract.Tasks.Request;
//using MS.Services.TaskCatalog.Contract.Tasks.Result;
//using MS.Services.TaskCatalog.Domain.SharedKernel;
//using MsftFramework.Abstractions.CQRS.Query;
//using MsftFramework.Core.Dependency;

//namespace MS.Services.TaskCatalog.Api.Tasks;

//// GET api/v1/taskCatalog/Tasks/{id}
//public static class GetUserProjectsEndpoint
//{
//    internal static IEndpointRouteBuilder MapGetUserProjectsEndpoint(this IEndpointRouteBuilder endpoints)
//    {
//        endpoints.MapGet(
//                $"{UsersConfigs.UsersPrefixUri}",
//                GetUserProjects)
//            .WithTags(UsersConfigs.Tag)
//            // .RequireAuthorization()
//            .Produces<GetTasksResult>(StatusCodes.Status200OK)
//            .Produces(StatusCodes.Status401Unauthorized)
//            .Produces(StatusCodes.Status400BadRequest)
//            .Produces(StatusCodes.Status404NotFound)
//            .WithName("GetTasks")
//            .WithDisplayName("Get Tasks");

//        return endpoints;
//    }
//    private static async Task<IResult> GetUserProjects(
//        long? projectId,
//        long? unitId,
//        DateTime? startDate,
//        DateTime? endDate,
//        priorityType? priorityType,
//        IQueryProcessor queryProcessor,
//        CancellationToken cancellationToken)
//    {
//        var result = await queryProcessor.SendAsync(new GetTasksQueryRequest(projectId,unitId,startDate,endDate,priorityType), cancellationToken);

//        return Results.Ok(result);
//    }
//}