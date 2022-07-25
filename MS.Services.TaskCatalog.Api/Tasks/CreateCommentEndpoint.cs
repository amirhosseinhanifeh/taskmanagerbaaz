using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract;
using MS.Services.TaskCatalog.Contract.Comments.Commands;
using MS.Services.TaskCatalog.Contract.Comments.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Security.Jwt;

namespace MS.Services.TaskCatalog.Api.Tasks;

public static class CreateCommentEndpoint
{
    internal static IEndpointRouteBuilder MapAddCommentEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{TasksConfigs.TasksPrefixUri}/{{taskId}}/comment/create", CreateComment)
            .WithTags(TasksConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateTaskResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Add comment")
            .WithDisplayName("Create a new Task comment.");

        return endpoints;
    }

    private static async Task<IResult> CreateComment(
      long taskId,
       CreateCommentRequest request,
       ISecurityContextAccessor securityContextAccessor,
       ICommandProcessor commandProcessor,
       IMapper mapper,
       CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var s = securityContextAccessor.UserId;
        var command = mapper.Map<CreateCommentCommand>(request);
        command.TaskId = taskId;
        var result = await commandProcessor.SendAsync(command, cancellationToken);

        return Results.Ok(result);
    }
}