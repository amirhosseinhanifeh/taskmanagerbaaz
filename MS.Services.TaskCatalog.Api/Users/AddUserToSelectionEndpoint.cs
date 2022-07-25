using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Contract.Users.Command;
using MS.Services.TaskCatalog.Contract.Users.Request;
using MS.Services.TaskCatalog.Contract.Users.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.Dependency;

namespace MS.Services.TaskCatalog.Api.Users;

// GET api/v1/taskCatalog/Tasks/{id}
public static class AddUserToSelectionEndpoint
{
    internal static IEndpointRouteBuilder MapAddUserToSelectionEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
                $"{UsersConfigs.UsersPrefixUri}/selections/{{userId}}/create",
                AddUserToSelection)
            .WithTags(UsersConfigs.Tag)
            // .RequireAuthorization()
            .Produces<AddUserToSelectionResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Add User To Selection")
            .WithDisplayName("Add Selection Users");

        return endpoints;
    }
    private static async Task<IResult> AddUserToSelection(
        long userId,
        ICommandProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        long currectuserId = 1;
        var result = await queryProcessor.SendAsync(new AddUserToSelectionCommand(currectuserId,userId), cancellationToken);

        return Results.Ok(result);
    }
}