using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Security.Jwt;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class AddTaskNotifTaskEndpoint
{
    internal static IEndpointRouteBuilder MapSendNotifTaskEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{TasksConfigs.TasksPrefixUri}/sendnotif", CreateTaskNotifTime)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Send Notif")
            .WithDisplayName("Create a new Task Notif.");

        return endpoints;
    }

    private static async Task<IResult> CreateTaskNotifTime(
       CreateTaskNotifRequest request,
       ISecurityContextAccessor securityContextAccessor,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var s = securityContextAccessor.UserId;
        var command = mapper.Map<AddTaskNotifCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.Ok(true);
    }
}