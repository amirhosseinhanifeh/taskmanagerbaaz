using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsftFramework.Caching.InMemory;
using MsftFramework.Core.AssemblyHelper;
using MsftFramework.Core.Caching;
using MsftFramework.Core.Extensions.Configuration;
using MsftFramework.Core.Extensions.DependencyInjection;
using MsftFramework.Core.IdsGenerator;
using MsftFramework.Core.Persistence.EfCore;
using MsftFramework.CQRS;
using MsftFramework.Email;
using MsftFramework.Logging;
using MsftFramework.Messaging.Postgres.Extensions;
using MsftFramework.Messaging.Transport.Rabbitmq;
using MsftFramework.Monitoring;
using MsftFramework.Persistence.EfCore.Postgres;
using MsftFramework.Scheduling.Internal.Extensions;
using MsftFramework.Validation;
using System.Reflection;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.ServiceCollectionExtensions;
public static class ServiceCollection
{
    public static WebApplicationBuilder AddInfrastructure(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        AddInfrastructure(builder.Services, configuration);

        return builder;
    }
    public static IEnumerable<Assembly> GetAssemblies()
    {
        var list = new List<string>();
        var stack = new Stack<Assembly>();

        stack.Push(Assembly.GetEntryAssembly());

        do
        {
            var asm = stack.Pop();

            yield return asm;

            foreach (var reference in asm.GetReferencedAssemblies().Where(e=>e.FullName.Contains("MS")))
                if (!list.Contains(reference.FullName))
                {
                    stack.Push(Assembly.Load(reference));
                    list.Add(reference.FullName);
                }

        }
        while (stack.Count > 0);

    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = GetAssemblies().ToArray();// AssemblyExtractor.GetDomainAssemblies("MS").ToArray();

        SnowFlakIdGenerator.Configure(2);
        services.AddCore(configuration, assemblies);

        services.AddPostgresMessaging(configuration);

        //services.AddInternalScheduler(configuration);

        services.AddRabbitMqTransport(configuration);

        services.AddEmailService(configuration);

        services.AddCqrs(assemblies, s =>
        {
            s.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamRequestValidationBehavior<,>))
                .AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamLoggingBehavior<,>))
                .AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamCachingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(InvalidateCachingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(EfTxBehavior<,>));
        });

        services.AddMonitoring(healthChecksBuilder =>
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>(nameof(PostgresOptions));
            healthChecksBuilder.AddNpgSql(
                postgresOptions.ConnectionString,
                name: "ms-services-taskCatalog-Postgres-Check",
                tags: new[] { "taskCatalog-postgres" });

            var rabbitMqOptions = configuration.GetOptions<RabbitConfiguration>(nameof(RabbitConfiguration));

            healthChecksBuilder.AddRabbitMQ(
                $"amqp://{rabbitMqOptions.UserName}:{rabbitMqOptions.Password}@{rabbitMqOptions.HostName}{rabbitMqOptions.VirtualHost}",
                name: "ms-services-taskCatalog-RabbitMQ-Check",
                tags: new[] { "taskCatalog-rabbitmq" });
        });
        foreach (var assembly in assemblies!)
            services.AddCustomValidators(assembly);

        services.AddAutoMapper(assemblies);

        services.AddCustomInMemoryCache(configuration)
            .AddCachingRequestPolicies(assemblies);

        return services;
    }
}