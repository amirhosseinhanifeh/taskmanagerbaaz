using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Scheduling.Hangfire;

namespace MS.Services.TaskCatalog.Api.workflows.WorkflowInstance;

public static class InitWorkflowEndpoint
{
    internal static IEndpointRouteBuilder MapInitWorkflowsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{WorkflowsInstanceConfigs.WorkflowInstancesPrefixUri}/Init", InitWorkflows)
            .WithTags(WorkflowsInstanceConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateWorkflowResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("InitWorkflow")
            .WithDisplayName("Create a new Workflow.");
        return endpoints;
    }

    private static async Task<IResult> InitWorkflows(
       InitWorkflowRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<InitWorkflowCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);
        return Results.Ok(result);
    }


}
