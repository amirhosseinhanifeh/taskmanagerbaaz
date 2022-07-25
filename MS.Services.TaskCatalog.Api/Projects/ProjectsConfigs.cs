using MS.Services.TaskCatalog.Application;
using MS.Services.TaskCatalog.Application.Projects;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Persistence;

namespace MS.Services.TaskCatalog.Api.Projects;

internal static class ProjectsConfigs
{
    public const string Tag = "Project";
    public const string ProjectsPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/projects";

    internal static IServiceCollection AddProjectsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, ProjectEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapProjectsEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapCreateProjectsEndpoint()
                 .MapGetProjectByIdEndpoint()
                 .MapGetProjectsEndpoint();
}
