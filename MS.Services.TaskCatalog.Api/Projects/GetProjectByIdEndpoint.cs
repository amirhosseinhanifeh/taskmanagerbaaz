using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Projects.Request;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Api.Projects;

// GET api/v1/TaskCatalog/projects/{id}
public static class GetProjectByIdEndpoint
{
    internal static IEndpointRouteBuilder MapGetProjectByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{ProjectsConfigs.ProjectsPrefixUri}/{{id}}",
                GetProjectById)
            .WithTags(ProjectsConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetProjectByIdResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetProjectById")
            .WithDisplayName("Get project By Id.");

        return endpoints;
    }
    private static async Task<IResult> GetProjectById(
        long id,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        var result = await queryProcessor.SendAsync(new GetProjectByIdQueryRequest(id), cancellationToken);

        return Results.Ok(result);
    }
}