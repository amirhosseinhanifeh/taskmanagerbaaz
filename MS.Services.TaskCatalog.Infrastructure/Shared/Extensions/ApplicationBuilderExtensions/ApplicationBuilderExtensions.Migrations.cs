using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.ApplicationBuilderExtensions;
public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (configuration.GetValue<bool>("UseInMemoryDatabase") == false)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var taskCatalogDbContext = serviceScope.ServiceProvider.GetRequiredService<TaskCatalogDbContext>();

            logger.LogInformation("Updating taskCatalog database...");

            await taskCatalogDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated taskCatalog database");
        }
    }
}