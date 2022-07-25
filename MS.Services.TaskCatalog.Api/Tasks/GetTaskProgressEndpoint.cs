using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Dependency;

namespace MS.Services.TaskCatalog.Api.Tasks;

// GET api/v1/taskCatalog/Tasks/{id}
public static class GetTaskProgressEndpoint
{
    internal static IEndpointRouteBuilder MapGetTaskProgressesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{TasksConfigs.TasksPrefixUri}/{{id}}/progresses",
                GetTaskProgresses)
            .WithTags(TasksConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetTaskProgressResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetTaskProgresses")
            .WithDisplayName("Get Task Progresses");

        return endpoints;
    }
    private static async Task<IResult> GetTaskProgresses(
        long id,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        var result = await queryProcessor.SendAsync(new GetTaskProgressQueryRequest(id), cancellationToken);

        return Results.Ok(result);
    }
}