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
public class CreateTaskHandler : ICommandHandler<CreateTaskCommand, CreateTaskResult>, IDisposable
{
    private readonly ILogger<CreateTaskHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

    public CreateTaskHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateTaskHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<CreateTaskResult>> Handle(
        CreateTaskCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var Task = Domain.Tasks.Task.Create(command.Id, command.name, command.startDateTime, command.endDateTime, command.priority, command.importanceType, command.description, command.voiceId, 1, command.controllerUserId, command.testerUserId, null);

        var uIds = await _taskCatalogDbContext.GetUsersByIds(command.userIds, cancellationToken);
        Task.AssignTaskToUsers(uIds.ToArray());

        var pIds = await _taskCatalogDbContext.GetProjectsByIds(command.projectIds, cancellationToken);
        Task.AddProject(pIds.ToArray());


        if (command.unitIds != null)
        {
            List<Unit> units = new List<Unit>();

            foreach (var item in command.unitIds)
            {
                var u = await _taskCatalogDbContext.Units.FirstOrDefaultAsync(h => h.Id == item);
                if (u != null)
                    units.Add(u);
            }
            Task.AddUnit(units.ToArray());
        }
        Task.AddRequirements(command.requirements);

        if (command.deadLine != null)
            Task.AddDeadLine(command.deadLine.Time, command.deadLine.Date);


        if (command.imageIds != null)
            Task.Images = new List<TaskImage>();
            foreach (var item in command.imageIds)
            {
                Task.Images.Add(new TaskImage
                {
                    ImageId = item,
                });
            }

        await _taskCatalogDbContext.Tasks.AddAsync(Task, cancellationToken: cancellationToken);
        try
        {

        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {

        }

        var created = await _taskCatalogDbContext.Tasks
                   .SingleOrDefaultAsync(x => x.Id == Task.Id, cancellationToken: cancellationToken);

        var TaskDto = _mapper.Map<TaskDto>(created);

        _logger.LogInformation("Task a with ID: '{TaskId} created.'", command.Id);

        var result = new Result();
        return result.ToResult(new CreateTaskResult(TaskDto));
    }

    public void Dispose()
    {
        _taskCatalogDbContext.Dispose();
    }
}