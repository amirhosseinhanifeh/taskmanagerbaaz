using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MsftFramework.Messaging;
using MsftFramework.Messaging.Postgres.Extensions;
using MsftFramework.Monitoring;
using MsftFramework.Scheduling.Internal.Extensions;
namespace MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.ApplicationBuilderExtensions;
public static partial class ApplicationBuilderExtensions
{
    public static async Task<IApplicationBuilder> UseInfrastructure(
        this IApplicationBuilder app,
        IWebHostEnvironment environment,
        ILogger logger)
    {
        app.UseMonitoring();
        await app.UsePostgresMessaging(logger);
        //await app.UseInternalScheduler(logger);

        return app;
    }
} 