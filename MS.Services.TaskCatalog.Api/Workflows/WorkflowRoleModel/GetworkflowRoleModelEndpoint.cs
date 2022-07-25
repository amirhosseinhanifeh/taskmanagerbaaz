using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Api.workflows.Workflow;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Scheduling.Hangfire;

namespace MS.Services.TaskCatalog.Api.workflows.WorkflowRoleModel;

public static class GetworkflowRoleModelEndpoint
{
    internal static IEndpointRouteBuilder MapGetWorkflowRoleModelEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{WorkflowsConfigs.WorkflowsPrefixUri}/rolemodel/get", GetWorkflowRoleModel)
            .WithTags(WorkflowsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateWorkflowResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("GetWorkflowRoleModel")
            .WithDisplayName("Get a new Workflow RoleModel");
        return endpoints;
    }

    private static async Task<IResult> GetWorkflowRoleModel(
        long? unitId,
       IQueryProcessor queryProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        var result = await queryProcessor.SendAsync(new GetWorkflowRoleModelRequest(unitId), cancellationToken);
        return Results.Ok(result);
    }






}
