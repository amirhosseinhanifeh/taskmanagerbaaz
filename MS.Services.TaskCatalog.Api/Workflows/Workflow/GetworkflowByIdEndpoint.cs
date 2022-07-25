using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Api.workflows.Workflow;

// GET api/v1/taskCatalog/Workflows/{id}
public static class GetWorkflowByIdEndpoint
{
    internal static IEndpointRouteBuilder MapGetWorkflowByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{WorkflowsConfigs.WorkflowsPrefixUri}/{{id}}",
                GetWorkflowById)
            .WithTags(WorkflowsConfigs.Tag)
            // .RequireAuthorization()
            .Produces<GetWorkflowResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetWorkflowById")
            .WithDisplayName("Get Workflow By Id.");

        return endpoints;
    }
    private static async Task<IResult> GetWorkflowById(
        long id,
        IQueryProcessor queryProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        var result = await queryProcessor.SendAsync(new GetWorkflowByIdQueryRequest(id), cancellationToken);

        return Results.Ok(result);
    }
}
