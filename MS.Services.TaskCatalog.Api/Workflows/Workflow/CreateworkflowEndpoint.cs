using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using MediatR;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Scheduling.Hangfire;

namespace MS.Services.TaskCatalog.Api.workflows.Workflow;

public static class CreateWorkflowEndpoint
{
    internal static IEndpointRouteBuilder MapCreateWorkflowsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{WorkflowsConfigs.WorkflowsPrefixUri}/create", CreateWorkflows)
            .WithTags(WorkflowsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateWorkflowResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("CreateWorkflow")
            .WithDisplayName("Create a new Workflow.");
        return endpoints;
    }

    private static async Task<Result<Unit>> CreateWorkflows(
       CreateWorkflowRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<CreateWorkflowCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);
        return result;
    }

 




}
