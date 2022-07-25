using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Abstractions.Persistence;
using MsftFramework.Persistence.EfCore.Postgres;
using MsftFramework.Persistence.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.ServiceCollectionExtensions;
public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddStorage(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        AddStorage(builder.Services, configuration);

        return builder;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        AddPostgresWriteStorage(services, configuration);
        AddMongoReadStorage(services, configuration);

        return services;
    }

    private static void AddPostgresWriteStorage(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("PostgresOptions.UseInMemory"))
        {
            services.AddDbContext<TaskCatalogDbContext>(options =>
                options.UseInMemoryDatabase("ms.serevices.taskCatalog"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TaskCatalogDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<TaskCatalogDbContext>(configuration);
        }

        services.AddScoped<ITaskCatalogDbContext>(provider => provider.GetRequiredService<TaskCatalogDbContext>());
    }

    private static void AddMongoReadStorage(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<TaskCatalogReadDbContext>(configuration);
    }
}
