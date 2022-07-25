using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using MS.Services.TaskCatalog.Api.workflows.Workflow;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Scheduling.Hangfire;

namespace MS.Services.TaskCatalog.Api.workflow.Alerts;

public static class GetAlertEndpoint
{
    internal static IEndpointRouteBuilder MapGetAlertEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{WorkflowsConfigs.WorkflowsPrefixUri}/alert/get", GetAlerts)
            .WithTags(WorkflowsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateWorkflowStepsResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Get alerts")
            .WithDisplayName("Get alerts.");
        return endpoints;
    }

    private static async Task<IResult> GetAlerts(
       IQueryProcessor queryProcessor,
       IMapper mapper,
       CancellationToken cancellationToken 
       )
    {

        var result = await queryProcessor.SendAsync(new GetWorkflowAlertQueryRequest(), cancellationToken);

        return Results.Ok(result);
    }
}
