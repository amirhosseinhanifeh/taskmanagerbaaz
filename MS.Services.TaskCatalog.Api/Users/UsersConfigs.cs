using MS.Services.TaskCatalog.Api.Users;
using MS.Services.TaskCatalog.Application.Tasks;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Persistence;

namespace MS.Services.TaskCatalog.Api.Tasks;

internal static class UsersConfigs
{
    public const string Tag = "User";
    public const string UsersPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/users";

    internal static IServiceCollection AddUsersServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, TaskEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapGetUsersEndpoint()
        .MapAddUserToSelectionEndpoint();
}
