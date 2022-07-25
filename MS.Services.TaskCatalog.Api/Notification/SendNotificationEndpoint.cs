using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.EnumBuilderExtensions;

namespace MS.Services.TaskCatalog.Api.Notifications;

// GET api/v1/taskCatalog/Tasks/{id}
public static class SendNotificationEndpoint
{
    internal static IEndpointRouteBuilder MapSendNotificationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
                $"{NotificationConfigs.NotificationPrefixUri}",
                SendNotification)
            .WithTags(NotificationConfigs.Tag)
            //.RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Send Notification")
            .WithDisplayName("Send Notification");

        return endpoints;
    }
    private static async Task<IResult> SendNotification(
        CancellationToken cancellationToken
        )
    {

        return Results.Ok();
    }
}
