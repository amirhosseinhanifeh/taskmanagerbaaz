using MS.Services.TaskCatalog.Application.Tasks;
using MsftFramework.Abstractions.Core.Domain.Events;

namespace MS.Services.TaskCatalog.Api.Categories
{
    internal static class CategoryConfigs
    {
        public const string Tag = "Category";
        public const string CategoryPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/categories";

        internal static IServiceCollection AddCategoriesServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventMapper, TaskEventMapper>();

            return services;
        }

        internal static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder endpoints) =>
            endpoints.MapCreateCategoriesEndpoint()
            .MapGetCategoriesEndpoint();
    }
}
