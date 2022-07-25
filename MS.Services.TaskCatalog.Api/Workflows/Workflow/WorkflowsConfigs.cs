using MS.Services.TaskCatalog.Api.workflow.Alerts;
using MS.Services.TaskCatalog.Api.workflows.WorkflowRoleModel;
using MS.Services.TaskCatalog.Application.workflows.Mappers.Workflow;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Persistence;

namespace MS.Services.TaskCatalog.Api.workflows.Workflow;

internal static class WorkflowsConfigs
{
    public const string Tag = "Workflow";
    public const string WorkflowsPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/workflows";

    internal static IServiceCollection AddWorkflowsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, WorkflowEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapWorkflowsEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapCreateWorkflowsEndpoint()
                 .MapGetWorkflowByIdEndpoint()
                 .MapGetWorkflowsEndpoint()
                 .MapCreateAlertEndpoint()
                 .MapGetAlertEndpoint()
                 .MapCreateWorkflowRoleModelEndpoint()
                 .MapGetWorkflowRoleModelEndpoint()
                 .MapAssignAlertToRoleEndpoint()
                ;
}
