using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.EnumBuilderExtensions;

namespace MS.Services.TaskCatalog.Api.Tasks;

// GET api/v1/taskCatalog/Tasks/{id}
public static class GetPriorityEndpoint
{
    internal static IEndpointRouteBuilder MapGetPriorityEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{PriorityConfigs.PriorityPrefixUri}",
                GetPriorities)
            .WithTags(PriorityConfigs.Tag)
            //.RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetPriorities")
            .WithDisplayName("Get Priorities");

        return endpoints;
    }
    private static async Task<IResult> GetPriorities(
        CancellationToken cancellationToken
        )
    {
        var result = Enum.GetValues(typeof(priorityType)).Cast<priorityType>().Select(x => new { Id = (int)x, Name=x.ToDisplay() });

        return Results.Ok(result);
    }
}
