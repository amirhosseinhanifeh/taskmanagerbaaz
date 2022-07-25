using MsftFramework.Abstractions.CQRS.Command;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MsftFramework.Abstractions.Persistence.Mongo;
using MsftFramework.Core.Exception;
using MsftFramework.Core.IdsGenerator;
using AutoMapper;
using MS.Services.TaskCatalog.Infrastructure;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Categories.Commands;
using MS.Services.TaskCatalog.Contract.Categories.Result;
using MS.Services.TaskCatalog.Contract.Categories.Dtos;
using MsftFramework.Abstractions.Persistence;
using FluentResults;
using MS.Services.TaskCatalog.Contract.Comments.Commands;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.FcmExtentions;

namespace MS.Services.TaskCatalog.Application.Categories.Features.Commands.Handlers;
public class CreateCommentHandler : ICommandHandler<CreateCommentCommand, bool>
{
    private readonly ILogger<CreateCategoryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    private readonly IFcmMessaging _fcmMessaging;

    public CreateCommentHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateCategoryHandler> logger, IFcmMessaging fcmMessaging)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
        _fcmMessaging = fcmMessaging;
    }


    public async Task<Result<bool>> Handle(
        CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        await _fcmMessaging.SendAsync("fAZcc5SpTpqKa9BTlr0q_2:APA91bGa9HGI8ukFXEI6mVTfPLfyua38qbFDJTZxnFbVAk3JmH3nfwXV9rcf0K2XGIzfrxWKzCt--EvCXGX-W1w9OSCfOjmPMHKJMcadzg-hYr5JldvPetfuoVIYhrtGbi5bqvoFFJ_6", "hi", "hi");


        // await _domainEventDispatcher.DispatchAsync(cancellationToken, new Events.Domain.CreatingTask());
        var Comment = Domain.Comments.Comment.Create(
            command.Id,
            command.Body,
            command.TaskId,
            command.CommentId
            );

        await _taskCatalogDbContext.Comments.AddAsync(Comment, cancellationToken: cancellationToken);

        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);

        //await MongoRepository.AddAsync(Task);

        _logger.LogInformation("Comments a with ID: '{CategoryId} created.'", command.Id);

        var result = new FluentResults.Result();

        return result.ToResult(true);
    }
}