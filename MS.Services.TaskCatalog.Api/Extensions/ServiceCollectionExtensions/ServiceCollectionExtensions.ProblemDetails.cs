using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using MsftFramework.Core.Dependency;
using MsftFramework.Core.Domain.Exceptions;
using MsftFramework.Core.Exception.Types;
using System.Text.Json;
using System.Net;
using System.Text;
using FluentResults;
using MS.Services.TaskCatalog.Domain.Resources;

namespace MS.Services.TaskCatalog.Api.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddCustomProblemDetails(this WebApplicationBuilder builder)
    {
        AddCustomProblemDetails(builder.Services);

        return builder;
    }

    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(x =>
        {
            x.ShouldLogUnhandledException = (httpContext, exception, problemDetails) =>
            {
                var env = httpContext.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };

            // Control when an exception is included
            x.IncludeExceptionDetails = (ctx, _) =>
            {
                // Fetch services from HttpContext.RequestServices
                var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };
            x.Map<ConflictException>(ex => new ProblemDetails
            {
                Title = "Application rule broken",
                Status = StatusCodes.Status409Conflict,
                Detail = ex.Message,
                Type = "https://www.baaz.com/application-rule-validation-error"
            });

            // Exception will produce and returns from our FluentValidation RequestValidationBehavior
           /* x.Map<MsftFramework.Validation.ValidationException>(ex =>
            {
                StringBuilder errorMessages = new();

                foreach (var m in ex.ValidationResultModel.Errors)
                {
                    var errors = m.Message;
                    errorMessages.AppendLine(errors);
                }

                var logger = ServiceActivator.GetService<ILoggerFactory>();
                logger!.CreateLogger<ILogger>()!.LogError(errorMessages.ToString(), ex);
                return new ProblemDetails
                {
                    Title = "input validation rules broken",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonConvert.SerializeObject(ex.ValidationResultModel.Errors),
                    Type = "https://www.baaz.com/input-validation-rules-error"
                };
            });*/
            x.Map<DomainException>(ex => new ProblemDetails
            {
                Title = "domain rules broken",
                Status = (int)ex.StatusCode,
                Detail = ex.Message,
                Type = "https://www.baaz.com/domain-rules-error"
            });
            x.Map<ArgumentException>(ex => new ProblemDetails
            {
                Title = "argument is invalid",
                Status = StatusCodes.Status400BadRequest,
                Detail = ex.Message,
                Type = "https://www.baaz.com/argument-error"
            });
            x.Map<BadRequestException>(ex => new ProblemDetails
            {
                Title = "bad request exception",
                Status = StatusCodes.Status400BadRequest,
                Detail = ex.Message,
                Type = "https://www.baaz.com/bad-request-error"
            });
            x.Map<NotFoundException>(ex => new ProblemDetails
            {
                Title = "not found exception",
                Status = (int)ex.StatusCode,
                Detail = ex.Message,
                Type = "https://www.baaz.com/not-found-error"
            });
            x.Map<ApiException>(ex => new ProblemDetails
            {
                Title = "api server exception",
                Status = (int)ex.StatusCode,
                Detail = ex.Message,
                Type = "https://www.baaz.com/api-server-error"
            });
            x.Map<AppException>(ex => new ProblemDetails
            {
                Title = "application exception",
                Status = (int)ex.StatusCode,
                Detail = ex.Message,
                Type = "https://www.baaz.com/application-error"
            });
            x.Map<IdentityException>(ex =>
            {
                var pd = new ProblemDetails
                {
                    Status = (int)ex.StatusCode,
                    Title = "identity exception",
                    Detail = ex.Message,
                    Type = "https://www.baaz.com/identity-error"
                };

                return pd;
            });
            x.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
        });
        return services;
    }
}



public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case AppException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            Result messageReslt = new Result();
            messageReslt.WithError(Messages.Failure).WithError(error!.Message);
            messageReslt.Log();
            var result = JsonSerializer.Serialize(messageReslt);

            await response.WriteAsync(result);
        }
    }
}