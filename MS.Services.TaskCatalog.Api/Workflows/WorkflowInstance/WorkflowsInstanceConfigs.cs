using MS.Services.TaskCatalog.Api.workflow.Alerts;
using MS.Services.TaskCatalog.Application.workflows.Mappers.Workflow;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Persistence;

namespace MS.Services.TaskCatalog.Api.workflows.WorkflowInstance;

internal static class WorkflowsInstanceConfigs
{
    public const string Tag = "WorkflowInstance";
    public const string WorkflowInstancesPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/workflowinstance";

    internal static IServiceCollection AddWorkflowsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, WorkflowEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapWorkflowInstancesEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints
        .MapGetWorkflowInstancesEndpoint()
        .MapInitWorkflowsEndpoint()
        .MapStartWorkflowByIdEndpoint()
        .MapResumeWorkflowEndpoint()
                ;
}
