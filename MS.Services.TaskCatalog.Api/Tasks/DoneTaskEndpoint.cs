using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class DoneTaskEndpoint
{
    internal static IEndpointRouteBuilder MapDoneTaskEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapMethods($"{TasksConfigs.TasksPrefixUri}/{{Id}}/done",new string[] {"Patch"}, DoneTask)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("DoneTask")
            .WithDisplayName("Done Task");

        return endpoints;
    }

    private static async Task<IResult> DoneTask(
       long Id,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(Id, nameof(Id));

        var result = await commandProcessor.SendAsync(new DoneTaskCommand(Id), cancellationToken);

        return Results.Ok(result);
    }
}