using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Security.Jwt;
using Newtonsoft.Json;

namespace MS.Services.TaskCatalog.Api.Tasks;

// GET api/v1/taskCatalog/Tasks/{id}
public static class GetTodayTasksEndpoint
{
    internal static IEndpointRouteBuilder MapGetTodayTasksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{TasksConfigs.TasksPrefixUri}/todaylist",
                GetTodayTasks)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<GetTodayTasksResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Get Today Tasks")
            .WithDisplayName("Get Today Tasks");

        return endpoints;
    }
    private static async Task<IResult> GetTodayTasks(
        ISecurityContextAccessor securityContextAccessor,
        string? name,
        string? projectIds,
        string? unitIds,
        string? userIds,
        long? controllerId,
        long? testerId,
        long? creatoruserId,
        DateTime? startDate,
        DateTime? endDate,
        priorityType? priorityType,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken,
        TaskSort sort = TaskSort.New,
        TodayOrderPriority orderPriority = TodayOrderPriority.User,
        int pageSize = 12,
        int page = 1

        )
    {
        long[]? projectId = null;
        if (!string.IsNullOrEmpty(projectIds))
            projectId = JsonConvert.DeserializeObject<long[]>(projectIds);

        long[]? userId = null;
        if (!string.IsNullOrEmpty(userIds))
            userId = JsonConvert.DeserializeObject<long[]>(userIds);

        long[]? unitId = null;
        if (!string.IsNullOrEmpty(unitIds))
            unitId = JsonConvert.DeserializeObject<long[]>(unitIds);

        var result = await queryProcessor.SendAsync(new GetTodayTasksQueryRequest(name, projectId, unitId, userId, controllerId, testerId, creatoruserId, startDate, endDate, priorityType, sort, pageSize, page, orderPriority), cancellationToken);

        return Results.Ok(new { tasks = result.Value.Tasks, totalItemCount = result.Value.TotalItemCount });
    }
}