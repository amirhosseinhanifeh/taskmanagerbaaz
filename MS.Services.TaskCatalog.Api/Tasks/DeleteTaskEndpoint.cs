using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class DeleteTaskEndpoint
{
    internal static IEndpointRouteBuilder MapDeleteTasksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete($"{TasksConfigs.TasksPrefixUri}/delete/{{id}}", Delete)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<FluentResults.Result<bool>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("DeleteTask")
            .WithDisplayName("Delete a new Task.");

        return endpoints;
    }

    private static async Task<IResult> Delete(
       long id,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        var result = await commandProcessor.SendAsync(new DeleteTaskCommand(id), cancellationToken);

        return Results.Ok(result);
    }
}