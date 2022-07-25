using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using MS.Services.TaskCatalog.Api.workflows.Workflow;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Scheduling.Hangfire;

namespace MS.Services.TaskCatalog.Api.workflow.Alerts;

public static class CreateAlertEndpoint
{
    internal static IEndpointRouteBuilder MapCreateAlertEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{WorkflowsConfigs.WorkflowsPrefixUri}/alert/create", CreateAlerts)
            .WithTags(WorkflowsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateWorkflowStepsResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Create alert")
            .WithDisplayName("Create a new alert.");
        return endpoints;
    }

    private static async Task<IResult> CreateAlerts(
       CreateAlertRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken 
       )
    {
        Guard.Against.Null(request, nameof(request));
        var command = mapper.Map<CreateAlertCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.Ok(result);
    }
}
