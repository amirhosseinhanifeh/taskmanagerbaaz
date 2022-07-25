

using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Api.workflows.Workflow;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Api.workflows.WorkflowInstance;

public static class ResumeWorkflowEndpoint
{
    internal static IEndpointRouteBuilder MapResumeWorkflowEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
                $"{WorkflowsInstanceConfigs.WorkflowInstancesPrefixUri}/resume/{{Id}}", ResumeWorkflow)
            .WithTags(WorkflowsInstanceConfigs.Tag)
            // .RequireAuthorization()
            .Produces<StartWorkflowCommand>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("ResumeWorkflow")
            .WithDisplayName("Resume Workflow.");

        return endpoints;
    }
    private static async Task<IResult> ResumeWorkflow( 
           ICommandProcessor commandProcessor,
           IMapper mapper,
           long Id,
           CancellationToken cancellationToken)
    { 

        var result = await commandProcessor.SendAsync(new ResumeWorkflowCommand(Id), cancellationToken);
        return Results.Ok(result);
    }
}
