using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class UpdateTaskEndpoint
{
    internal static IEndpointRouteBuilder MapUpdateTasksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut($"{TasksConfigs.TasksPrefixUri}/update/{{id}}", Update)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("UpdateTask")
            .WithDisplayName("Update a new Task.");

        return endpoints;
    }

    private static async Task<IResult> Update(
       long? id,
       UpdateTaskRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<UpdateTaskCommand>(request);
        command.Id = id.GetValueOrDefault();
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.CreatedAtRoute("GetTaskById", new { id = result.Value.Task.Id }, result);
    }
}