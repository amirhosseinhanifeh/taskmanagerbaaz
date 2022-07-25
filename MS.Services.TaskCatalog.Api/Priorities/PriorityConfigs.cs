using MS.Services.TaskCatalog.Application.Tasks;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Persistence;

namespace MS.Services.TaskCatalog.Api.Tasks;

internal static class PriorityConfigs
{
    public const string Tag = "Priority";
    public const string PriorityPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/priority";

    internal static IServiceCollection AddPriorityServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, TaskEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapPrioritiesEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapGetPriorityEndpoint();
}
