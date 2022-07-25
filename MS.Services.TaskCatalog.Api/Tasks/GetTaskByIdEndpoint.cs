using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Api.Tasks;

// GET api/v1/taskCatalog/Tasks/{id}
public static class GetTaskByIdEndpoint
{
    internal static IEndpointRouteBuilder MapGetTaskByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{TasksConfigs.TasksPrefixUri}/{{id}}",
                GetTaskById)
            .WithTags(TasksConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetTaskByIdResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetTaskById")
            .WithDisplayName("Get Task By Id.");

        return endpoints;
    }
    private static async Task<IResult> GetTaskById(
        long id,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        var result = await queryProcessor.SendAsync(new GetTaskByIdQueryRequest(id), cancellationToken);

        return Results.Ok(result);
    }
}