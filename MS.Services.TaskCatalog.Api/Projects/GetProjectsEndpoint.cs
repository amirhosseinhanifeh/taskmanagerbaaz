using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Projects.Request;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Dependency;

namespace MS.Services.TaskCatalog.Api.Projects;

// GET api/v1/taskCatalog/Tasks/{id}
public static class GetProjectsEndpoint
{
    internal static IEndpointRouteBuilder MapGetProjectsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{ProjectsConfigs.ProjectsPrefixUri}",
                GetProjects)
            .WithTags(ProjectsConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetProjectResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetProjects")
            .WithDisplayName("Get projects");

        return endpoints;
    }
    /// <summary>
    /// لیست
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="queryProcessor"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> GetProjects(
        bool? hasSelection,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        var result = await queryProcessor.SendAsync(new GetProjectsQueryRequest(hasSelection), cancellationToken);

        return Results.Ok(result);
    }
}