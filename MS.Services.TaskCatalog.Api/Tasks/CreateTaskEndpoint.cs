using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Security.Jwt;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class CreateTaskEndpoint
{
    internal static IEndpointRouteBuilder MapCreateTasksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{TasksConfigs.TasksPrefixUri}/create", CreateTasks)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("CreateTask")
            .WithDisplayName("Create a new Task.");

        return endpoints;
    }

    private static async Task<IResult> CreateTasks(
       CreateTaskRequest request,
       ISecurityContextAccessor securityContextAccessor,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var s = securityContextAccessor.UserId;
        var command = mapper.Map<CreateTaskCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.CreatedAtRoute("GetTaskById", new { id = result.Value.Task.Id }, result);
    }
}