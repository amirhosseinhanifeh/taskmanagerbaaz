using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class UpdateTaskTodayEndpoint
{
    internal static IEndpointRouteBuilder MapUpdateTaskTodayEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut($"{TasksConfigs.TasksPrefixUri}/update/today/update", UpdateTaskToday)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Update Task Today")
            .WithDisplayName("Update Task Today");

        return endpoints;
    }

    private static async Task<IResult> UpdateTaskToday(
       UpdateTaskTodayRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<UpdateTaskTodayCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.Ok(result);
    }
}