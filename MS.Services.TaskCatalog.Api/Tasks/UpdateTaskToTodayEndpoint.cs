using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class UpdateTaskToTodayEndpoint
{
    internal static IEndpointRouteBuilder MapUpdateToTodayEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapMethods($"{TasksConfigs.TasksPrefixUri}/update/today",new string[] {"Patch"}, UpdateTaskToToday)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("UpdateToToday")
            .WithDisplayName("Update Task To Today");

        return endpoints;
    }

    private static async Task<IResult> UpdateTaskToToday(
       UpdateTaskToTodayRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<UpdateTaskToTodayCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.Ok(result);
    }
}