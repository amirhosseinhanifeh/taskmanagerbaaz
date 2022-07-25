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

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Commands.Handlers;
public class UpdateTaskToTodayHandler : ICommandHandler<UpdateTaskToTodayCommand, bool>
{
    private readonly ILogger<CreateTaskHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

    public UpdateTaskToTodayHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateTaskHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<bool>> Handle(
        UpdateTaskToTodayCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));


        var created = await _taskCatalogDbContext.Tasks
                   .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);
        if (created != null)
        {
            created.SetTodayTask(command.IsToday);

            if (command.IsToday)
                created.ChangeDate(DateTime.Now);
            else
                created.ChangeStatus(null);
            await _taskCatalogDbContext.SaveChangesAsync();
        }

        var result = new FluentResults.Result();

        return result.ToResult(true);
    }
}
