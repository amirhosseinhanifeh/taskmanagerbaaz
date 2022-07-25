
using MS.Services.TaskCatalog.Api.Categories;
using MS.Services.TaskCatalog.Api.Notifications;
using MS.Services.TaskCatalog.Api.Projects;
using MS.Services.TaskCatalog.Api.Tasks;
using MS.Services.TaskCatalog.Api.workflow.Alerts;
using MS.Services.TaskCatalog.Api.workflows.Workflow;
using MS.Services.TaskCatalog.Api.workflows.WorkflowInstance;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.ApplicationBuilderExtensions;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.ServiceCollectionExtensions;

namespace MS.Services.TaskCatalog.Api;


public static class TaskCatalogConfigurations
{
    public const string TaskCatalogModulePrefixUri = "api/v1/taskCatalog";

    public static WebApplicationBuilder AddTaskCatalogModuleServices(this WebApplicationBuilder builder)
    {
        AddTaskCatalogModuleServices(builder.Services, builder.Configuration);

        return builder;
    }

    public static IServiceCollection AddTaskCatalogModuleServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddStorage(configuration);

        services.AddTasksServices()
                .AddCategoriesServices()
                .AddProjectsServices()
                .AddUsersServices()
                .AddPriorityServices()
                .AddImportanceServices();


        return services;
    }

    public static IEndpointRouteBuilder MapTaskCatalogModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"ms-services-taskCatalog services Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();

        endpoints
            .MapTasksEndpoints()
            .MapCategoriesEndpoints()
            .MapProjectsEndpoints()
            .MapUserEndpoints()
            .MapWorkflowsEndpoints()
            .MapPrioritiesEndpoints()
            .MapImportancesEndpoints()
            .MapWorkflowInstancesEndpoints()
            .MapNotificationEndpoints()
        ;

        return endpoints;
    }

    public static async Task ConfigureTaskCatalogModule(
        this IApplicationBuilder app,
        IWebHostEnvironment environment,
        ILogger logger)
    {
        await app.UseInfrastructure(environment, logger);

        await app.ApplyDatabaseMigrations(logger);
        //await app.SeedData(logger, environment);
    }
}