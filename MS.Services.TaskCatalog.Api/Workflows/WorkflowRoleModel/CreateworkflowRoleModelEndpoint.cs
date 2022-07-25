using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Api.workflows.Workflow;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Scheduling.Hangfire;

namespace MS.Services.TaskCatalog.Api.workflows.WorkflowRoleModel;

public static class CreateworkflowRoleModelEndpoint
{
    internal static IEndpointRouteBuilder MapCreateWorkflowRoleModelEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{WorkflowsConfigs.WorkflowsPrefixUri}/rolemodel/create", CreateWorkflowRoleModel)
            .WithTags(WorkflowsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateWorkflowResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("CreateWorkflowRoleModel")
            .WithDisplayName("Create a new Workflow RoleModel");
        return endpoints;
    }

    private static async Task<IResult> CreateWorkflowRoleModel(
       CreateworkflowRoleModelRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<CreatetWorkflowRoleModelCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);
        return Results.CreatedAtRoute("CreateWorkflow", new { id = result.Value.RoleModels }, result);
    }

 




}
