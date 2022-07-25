using MsftFramework.Abstractions.CQRS.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MsftFramework.Core.Exception;
using AutoMapper;
using MS.Services.TaskCatalog.Infrastructure;
using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Commands.Handlers;

public class UpdateTaskTodayHandler : ICommandHandler<UpdateTaskTodayCommand, bool>
{
    private readonly ILogger<UpdateTaskTodayHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

    public UpdateTaskTodayHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<UpdateTaskTodayHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<bool>> Handle(
        UpdateTaskTodayCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));


        var created = await _taskCatalogDbContext.Tasks
            .Include(x=>x.Users)
            .Include(x=>x.TaskProgresses)
                   .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);
        if (created != null)
        {
            created.UpdateTaskToday(command.AlertRemainder, command.UserStartTime, command.UserEndTime, command.CEndTime, command.CProgress, command.UserProgress);

            await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);
        }

        var result = new FluentResults.Result();

        return result.ToResult(true);
    }
}