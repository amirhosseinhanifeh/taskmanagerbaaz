using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class UpdateTaskOrderEndpoint
{
    internal static IEndpointRouteBuilder MapUpdateOrderTasksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapMethods($"{TasksConfigs.TasksPrefixUri}/update/order", new string[] { "Patch" }, UpdateTaskOrder)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("UpdateOrderTask")
            .WithDisplayName("Update Order a Task.");

        return endpoints;
    }

    private static async Task<IResult> UpdateTaskOrder(
       UpdateTaskOrderRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var command = mapper.Map<UpdateTaskOrderCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.Ok(result);
    }
}