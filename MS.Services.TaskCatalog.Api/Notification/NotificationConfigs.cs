using MS.Services.TaskCatalog.Application.Tasks;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Persistence;

namespace MS.Services.TaskCatalog.Api.Notifications;

internal static class NotificationConfigs
{
    public const string Tag = "Notification";
    public const string NotificationPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/notification";

    internal static IServiceCollection AddNotificationServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, TaskEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapNotificationEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapSendNotificationEndpoint();
}
