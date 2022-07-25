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
using MS.Services.TaskCatalog.Domain.Projects;
using FluentResults;

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Commands.Handlers;
public class DeleteTaskHandler : ICommandHandler<DeleteTaskCommand, string>
{
    private readonly ILogger<CreateTaskHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

    public DeleteTaskHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateTaskHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<string>> Handle(
        DeleteTaskCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var task = await _taskCatalogDbContext.Tasks
                   .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

        var result = new Result<string>();

        if (task != null)
        {
            _taskCatalogDbContext.Tasks.Remove(task);

            await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Task a with ID: '{TaskId} deleted.'", command.Id);
            return Result.Ok("تسک با موفقیت حذف شد");
        }
        return result.WithError("تسک");

    }
}