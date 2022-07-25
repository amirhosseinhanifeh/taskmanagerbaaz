using MS.Services.TaskCatalog.Application.Tasks;
using MsftFramework.Abstractions.Core.Domain.Events;

namespace MS.Services.TaskCatalog.Api.Tasks;

internal static class ImportanceConfigs
{
    public const string Tag = "Importance";
    public const string ImportancePrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/importance";

    internal static IServiceCollection AddImportanceServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, TaskEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapImportancesEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapGetImportanceEndpoint();
}