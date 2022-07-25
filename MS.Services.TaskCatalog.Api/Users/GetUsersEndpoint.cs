using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Contract.Users.Request;
using MS.Services.TaskCatalog.Contract.Users.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Dependency;

namespace MS.Services.TaskCatalog.Api.Users;

// GET api/v1/taskCatalog/Tasks/{id}
public static class GetUsersEndpoint
{
    internal static IEndpointRouteBuilder MapGetUsersEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{UsersConfigs.UsersPrefixUri}/selections",
                GetUsers)
            .WithTags(UsersConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetUsersResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Get Selection Users")
            .WithDisplayName("Get Selection Users");

        return endpoints;
    }
    private static async Task<IResult> GetUsers(
        bool? hasSelection,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        var result = await queryProcessor.SendAsync(new GetUsersQueryRequest(hasSelection), cancellationToken);

        return Results.Ok(result);
    }
}