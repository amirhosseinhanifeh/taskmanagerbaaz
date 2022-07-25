using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Projects.Commands;
using MS.Services.TaskCatalog.Contract.Projects.Request;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Projects;

public static class CreateProjectEndpoint
{
    internal static IEndpointRouteBuilder MapCreateProjectsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{ProjectsConfigs.ProjectsPrefixUri}/create", CreateProjects)
            .WithTags(ProjectsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateProjectResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("CreateProject")
            .WithDisplayName("Create a new project.");

        return endpoints;
    }

    private static async Task<IResult> CreateProjects(
       CreateProjectRequest request,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = mapper.Map<CreateProjectCommand>(request);
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.CreatedAtRoute("GetProjectById", new { id = result.Value.Project.Id }, result);
    }
}