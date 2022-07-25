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

public static class AssignAlertToRoleEndpoint
{
    internal static IEndpointRouteBuilder MapAssignAlertToRoleEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{WorkflowsConfigs.WorkflowsPrefixUri}/role/alert/assign/{{roleId}}", AssignAlertToRole)
            .WithTags(WorkflowsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateWorkflowResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Add Alert To Role")
            .WithDisplayName("Create a new Workflow.");
        return endpoints;
    }

    private static async Task<IResult> AssignAlertToRole(
        long roleId,
       AssingAlertToRoleRequest[] request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<AssignAlertToRoleCommand>(request);
        command.RoleId = roleId;
        var result = await commandProcessor.SendAsync(command, cancellationToken);
        return Results.Ok(true);
    }

}
