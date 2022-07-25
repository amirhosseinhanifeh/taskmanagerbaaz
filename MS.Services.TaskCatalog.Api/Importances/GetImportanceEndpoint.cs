using MS.Services.TaskCatalog.Api.Hubs;
using MS.Services.TaskCatalog.Application.Hubs;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.EnumBuilderExtensions;
using Microsoft.AspNetCore.SignalR.Client;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class GetImportanceEndpoint
{

    public static HubConnection connection;
    internal static IEndpointRouteBuilder MapGetImportanceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{ImportanceConfigs.ImportancePrefixUri}",
                GetImportances)
            .WithTags(ImportanceConfigs.Tag)
            //.RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetImportances")
            .WithDisplayName("Get Importances");

        return endpoints;
    }
    private static async Task<IResult> GetImportances(
        CancellationToken cancellationToken
        )
    {
        var result = EnumExtension.CastToList<ImportanceType>(typeof(ImportanceType)).Select(x => new { Id = (int)x, Value = x.ToDisplay(), Name = x.ToString() });


        return Results.Ok(result);
    }
}