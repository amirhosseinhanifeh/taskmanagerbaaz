using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Api.workflows.Workflow;

public static class GetWorkflowsEndpoint
{
    internal static IEndpointRouteBuilder MapGetWorkflowsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{WorkflowsConfigs.WorkflowsPrefixUri}/Get",
                GetWorkflows)
            .WithTags(WorkflowsConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetWorkflowResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetWorkflows")
            .WithDisplayName("Get Workflows.");

        return endpoints;
    }
    private static async Task<IResult> GetWorkflows(
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        var result = await queryProcessor.SendAsync(new GetWorkflowQueryRequest(), cancellationToken);

        return Results.Ok(result);
    }
}