using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Security.Jwt;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class FireTaskReportEndpoint
{
    internal static IEndpointRouteBuilder MapFireTaskReportEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{TasksConfigs.TasksPrefixUri}/fire", FireReportTasks)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("FireTask")
            .WithDisplayName("Create a new Task Fire.");

        return endpoints;
    }

    private static async Task<IResult> FireReportTasks(
       ISecurityContextAccessor securityContextAccessor,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    { 
        var result = await commandProcessor.SendAsync(new FireTaskCommand(), cancellationToken);
        return Results.Ok(result);
    }
}