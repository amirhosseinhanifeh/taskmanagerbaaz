using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Application.Hubs;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Api.workflows.WorkflowInstance;

public static class GetWorkflowInstancesEndpoint
{
    internal static IEndpointRouteBuilder MapGetWorkflowInstancesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{WorkflowsInstanceConfigs.WorkflowInstancesPrefixUri}/Get",
                GetWorkflowIntances)
            .WithTags(WorkflowsInstanceConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetWorkflowResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetWorkflowInstances")
            .WithDisplayName("Get Workflows Instances.");

        return endpoints;
    }
    private static async Task<IResult> GetWorkflowIntances(
        long? workflowId,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        var result = await queryProcessor.SendAsync(new GetWorkflowInstanceQueryRequest(workflowId), cancellationToken);

        //await WorkflowInstanceHub.Connect();
        return Results.Ok(result);
    }
}