using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using MS.Services.TaskCatalog.Api;
using MS.Services.TaskCatalog.Api.Extensions.ApplicationBuilderExtensions;
using MS.Services.TaskCatalog.Api.Extensions.ServiceCollectionExtensions;
using MS.Services.TaskCatalog.Api.Shared;
using MsftFramework.Core.Dependency;
using MsftFramework.Core.Exception.Types;
using MsftFramework.Logging;
using MsftFramework.Security;
using MsftFramework.Security.Jwt;
using MsftFramework.Swagger;
using MsftFramework.Web;
using MsftFramework.Web.Extensions.ApplicationBuilderExtensions;
using MsftFramework.Web.Extensions.ServiceCollectionExtensions;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MsftFramework.Scheduling.Hangfire;
using MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Handlers;
using MsftFramework.Caching.Redis;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.FcmExtentions;
using MS.Services.TaskCatalog.Application.Hubs;
using MS.Services.TaskCatalog.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((env, c) =>
{
    // Handling Captive Dependency Problem
    if (env.HostingEnvironment.IsDevelopment() || env.HostingEnvironment.IsEnvironment("tests") ||
        env.HostingEnvironment.IsStaging())
    {
        c.ValidateScopes = true;
    }
});
//builder.Services.AddScoped<>();
builder.Services.AddControllers(options =>
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())))
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddApplicationOptions(builder.Configuration);
var loggingOptions = builder.Configuration.GetSection(nameof(LoggerOptions)).Get<LoggerOptions>();

builder.AddCompression();
builder.AddCustomProblemDetails();
builder.Services.AddMediatR(typeof(CreateWorkflowCommandHandler).GetTypeInfo().Assembly);
builder.Host.AddCustomSerilog(config =>
{
    config.WriteTo.File(
        Program.GetLogPath(builder.Environment, loggingOptions) ?? "../logs/ms.services.taskCatalog.log",
        outputTemplate: loggingOptions?.LogTemplate ??
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true);
});
builder.Services.Configure<FcmConfigOptions>(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddCustomRedisCache(builder.Configuration);
builder.AddCustomSwagger(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();
builder.Services.AddHangfireScheduler(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddCustomRedisCache(builder.Configuration);
builder.Services.AddCustomJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IFcmMessaging, FcmMessaging>();


builder.Services.AddCustomAuthorization(
    rolePolicies: new List<RolePolicy>
    {
        new()
        {
            Name = TaskCatalogConstants.Role.Admin, Roles = new List<string> { TaskCatalogConstants.Role.Admin }
        },
        new()
        {
            Name = TaskCatalogConstants.Role.User, Roles = new List<string> { TaskCatalogConstants.Role.User }
        }
    });

builder.AddTaskCatalogModuleServices();


var app = builder.Build();

var environment = app.Environment;

//if (environment.IsDevelopment() || environment.IsEnvironment("docker"))
//{
app.UseDeveloperExceptionPage();

// Minimal Api not supported versioning in .net 6
app.UseCustomSwagger();
//}

app.UseHangfireScheduler();
app.UseAccessTokenValidator();

ServiceActivator.Configure(app.Services);

//app.UseProblemDetails();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAccessTokenValidator();
app.UseRouting();

app.UseAppCors();

app.UseSerilogRequestLogging();

app.UseCustomHealthCheck();

await app.ConfigureTaskCatalogModule(environment, app.Logger);

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<WorkflowHub>("/workflowhub");
//app.MapHub<ChatHub>("/chathub");
app.MapControllers();
app.UseHangfireScheduler();
app.MapTaskCatalogModuleEndpoints();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

await app.RunAsync();


public partial class Program
{
    public static string? GetLogPath(IWebHostEnvironment env, LoggerOptions loggerOptions)
        => env.IsDevelopment() ? loggerOptions.DevelopmentLogPath : loggerOptions.ProductionLogPath;
}

public static class sadchsbdc
{
    public static IServiceCollection AddCustomJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            TokenStorageType storageType = TokenStorageType.InMemory,
            Action<JwtBearerOptions>? optionsFactory = null,
            Action<JwtOptions>? jwtOptionsConfigure = null)
    {
        // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/415
        // https://mderriey.com/2019/06/23/where-are-my-jwt-claims/
        // https://leastprivilege.com/2017/11/15/missing-claims-in-the-asp-net-core-2-openid-connect-handler/
        // https://stackoverflow.com/a/50012477/581476
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        // Configuration Setup
        var options = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        services.AddSingleton(options);
        if (jwtOptionsConfigure is { })
            jwtOptionsConfigure.Invoke(options);
        else
            services.AddSingleton<IAccessTokenService, DistributedTokenService>();
        services.AddScoped<AccessTokenValidatorMiddleware>();
        services.AddSingleton<IJwtHandler, JwtHandler>();
        var tokenValidationParameters = new TokenValidationParameters
        {
            RequireAudience = options.RequireAudience,
            ValidIssuer = options.ValidIssuer,
            ValidIssuers = options.ValidIssuers,
            ValidateActor = options.ValidateActor,
            ValidAudience = options.ValidAudience,
            ValidAudiences = options.ValidAudiences,
            ValidateAudience = options.ValidateAudience,
            ValidateIssuer = options.ValidateIssuer,
            ValidateLifetime = options.ValidateLifetime,
            ValidateTokenReplay = options.ValidateTokenReplay,
            ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
            SaveSigninToken = options.SaveSigninToken,
            RequireExpirationTime = options.RequireExpirationTime,
            RequireSignedTokens = options.RequireSignedTokens,
            ClockSkew = TimeSpan.Zero
        };
        if (!string.IsNullOrWhiteSpace(options.AuthenticationType))
            tokenValidationParameters.AuthenticationType = options.AuthenticationType;
        var hasCertificate = false;
        if (options.Certificate is { })
        {
            X509Certificate2 certificate = null;
            var password = options.Certificate.Password;
            var hasPassword = !string.IsNullOrWhiteSpace(password);
            if (!string.IsNullOrWhiteSpace(options.Certificate.Location))
            {
                certificate = hasPassword
                    ? new X509Certificate2(options.Certificate.Location, password)
                    : new X509Certificate2(options.Certificate.Location);
                var keyType = certificate.HasPrivateKey ? "with private key" : "with public key only";
                Log.Information(
                    "Loaded X.509 certificate from location: '{Location}' {KeyType}", options.Certificate.Location,
                    keyType);
            }
            if (!string.IsNullOrWhiteSpace(options.Certificate.RawData))
            {
                var rawData = Convert.FromBase64String(options.Certificate.RawData);
                certificate = hasPassword
                    ? new X509Certificate2(rawData, password)
                    : new X509Certificate2(rawData);
                var keyType = certificate.HasPrivateKey ? "with private key" : "with public key only";
                Log.Information("Loaded X.509 certificate from raw data {KeyType}", keyType);
            }
            if (certificate is { })
            {
                if (string.IsNullOrWhiteSpace(options.Algorithm))
                    options.Algorithm = SecurityAlgorithms.RsaSha256;
                hasCertificate = true;
                tokenValidationParameters.IssuerSigningKey = new X509SecurityKey(certificate);
                var actionType = certificate.HasPrivateKey ? "issuing" : "validating";
                Log.Information("Using asymmetric encryption and X.509 certificate for {ActionType} tokens",
                    actionType);
            }
        }
        if (!string.IsNullOrWhiteSpace(options.SecretKey) && !hasCertificate)
        {
            if (string.IsNullOrWhiteSpace(options.Algorithm) || hasCertificate)
                options.Algorithm = SecurityAlgorithms.HmacSha256;
            var rawKey = Encoding.UTF8.GetBytes(options.SecretKey);
            tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);
            Log.Information("Using symmetric encryption for issuing tokens");
        }
        if (!string.IsNullOrWhiteSpace(options.NameClaimType))
            tokenValidationParameters.NameClaimType = options.NameClaimType;
        if (!string.IsNullOrWhiteSpace(options.RoleClaimType))
            tokenValidationParameters.RoleClaimType = options.RoleClaimType;
        // https://docs.microsoft.com/en-us/aspnet/core/security/authentication
        services.AddAuthentication(authOptions =>
        {
            // will choose bellow JwtBearer handler for handling authentication because of our default schema to `JwtBearerDefaults.AuthenticationScheme` we could another schemas with their handlers
            authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(bearer =>
            {
                //-- JwtBearerDefaults.AuthenticationScheme --
                bearer.Authority = options.Authority;
                bearer.Audience = options.Audience;
                bearer.MetadataAddress = options.MetadataAddress;
                bearer.SaveToken = options.SaveToken;
                bearer.RefreshOnIssuerKeyNotFound = options.RefreshOnIssuerKeyNotFound;
                bearer.RequireHttpsMetadata = options.RequireHttpsMetadata;
                bearer.IncludeErrorDetails = options.IncludeErrorDetails;
                bearer.TokenValidationParameters = tokenValidationParameters;
                if (!string.IsNullOrWhiteSpace(options.Challenge))
                    bearer.Challenge = options.Challenge;
                bearer.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            throw new IdentityException(
                                "The Token is expired.",
                                statusCode: HttpStatusCode.Unauthorized);
                        }
                        throw new IdentityException(
                            context.Exception.Message,
                            statusCode: HttpStatusCode.InternalServerError);
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (!context.Response.HasStarted)
                        {
                            throw new IdentityException(
                                "You are not Authorized.",
                                statusCode: HttpStatusCode.Unauthorized);
                        }
                        return Task.CompletedTask;
                    },
                    OnForbidden = _ =>
                        throw new IdentityException(
                            "You are not authorized to access this resource.",
                            statusCode: HttpStatusCode.Forbidden)
                };
                optionsFactory?.Invoke(bearer);
            })
            //.AddOpenIdConnect(option =>
            //{
            //    option.ClientId = options.ClientId;
            //    option.ClientSecret = options.ClientSecret;
            //    option.ResponseType = options.ResponseType;
            //})
            ;
        services.AddSingleton(tokenValidationParameters);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(JwtAuthBehavior<,>));
        services.AddScoped<ISecurityContextAccessor, SecurityContextAccessor>();
        return services;
    }
}