using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Categories;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Categories.Request;
using MS.Services.TaskCatalog.Contract.Projects.Request;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Dependency;

namespace MS.Services.TaskCatalog.Api.Categories;

// GET api/v1/taskCatalog/Tasks/{id}
public static class GetCategoriesEndpoint
{
    internal static IEndpointRouteBuilder MapGetCategoriesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{CategoryConfigs.CategoryPrefixUri}",
                GetCategories)
            .WithTags(CategoryConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetProjectResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetCategories")
            .WithDisplayName("Get Categories");

        return endpoints;
    }
    private static async Task<IResult> GetCategories(
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        var result = await queryProcessor.SendAsync(new GetCategoriesQueryRequest(), cancellationToken);

        return Results.Ok(result);
    }
}