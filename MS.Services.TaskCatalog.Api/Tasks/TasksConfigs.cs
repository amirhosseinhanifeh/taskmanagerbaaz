using MS.Services.TaskCatalog.Application.Tasks;
using MS.Services.TaskCatalog.Application.Units;
using MsftFramework.Abstractions.Core.Domain.Events;
using MsftFramework.Abstractions.Persistence;

namespace MS.Services.TaskCatalog.Api.Tasks;

internal static class TasksConfigs
{
    public const string Tag = "Task";
    public const string TasksPrefixUri = $"{TaskCatalogConfigurations.TaskCatalogModulePrefixUri}/tasks";

    internal static IServiceCollection AddTasksServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, TaskEventMapper>();
        services.AddSingleton<IEventMapper, UnitEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapTasksEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapCreateTasksEndpoint()
                 .MapGetTasksEndpoint()
                 .MapGetTaskByIdEndpoint()
                 .MapGetTaskProgressesEndpoint()
                 .MapUpdateTasksEndpoint()
                 .MapDeleteTasksEndpoint()
                 .MapUpdateOrderTasksEndpoint()
                 .MapUpdateToTodayEndpoint()
                 .MapFireTaskReportEndpoint()
                 .MapSendNotifTaskEndpoint()
                 .MapGetTodayTasksEndpoint()
                 .MapAddCommentEndpoint()
                 .MapDoneTaskEndpoint()
                 .MapUpdateTaskTodayEndpoint()
        ;
}
