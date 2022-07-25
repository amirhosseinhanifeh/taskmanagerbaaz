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
public class UpdateTaskHandler : ICommandHandler<UpdateTaskCommand, CreateTaskResult>
{
    private readonly ILogger<CreateTaskHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

    public UpdateTaskHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateTaskHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<CreateTaskResult>> Handle(
        UpdateTaskCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var task = await _taskCatalogDbContext.Tasks.Include(x=>x.Requirements).Include(x=>x.Users).Include(x=>x.Projects).Include(x=>x.Units).SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        Guard.Against.Null(task);
        task.Update(command.Id, command.name, command.startDateTime, command.endDateTime, command.priority, command.importanceType, command.description, command.voiceId, 1, command.controllerUserId, command.testerUserId);

        if (command.userIds != null)
        {
            List<User> users = new List<User>();
            foreach (var item in command.userIds)
            {
                var u = await _taskCatalogDbContext.Users.FirstOrDefaultAsync(h => h.Id == item);
                if (u != null)
                    users.Add(u);
            }
            task.Users.Clear();
            task.AssignTaskToUsers(users.ToArray());
        }

        if (command.projectIds != null)
        {
            List<Project> projects = new List<Project>();

            foreach (var item in command.projectIds)
            {
                var u = await _taskCatalogDbContext.Projects.FirstOrDefaultAsync(h => h.Id == item);
                if (u != null)
                {

                    projects.Add(u);
                }
            }
            task.Projects.Clear();
            task.AddProject(projects.ToArray());
        }
        if (command.unitIds != null)
        {
            List<Unit> units = new List<Unit>();

            foreach (var item in command.unitIds)
            {
                var u = await _taskCatalogDbContext.Units.FirstOrDefaultAsync(h => h.Id == item);
                if (u != null)
                    units.Add(u);
            }
            task.Units.Clear();
            task.AddUnit(units.ToArray());
        }
        task.Requirements.Clear();
        task.AddRequirements(command.requirements);

        if (command.deadLine != null)
            task.AddDeadLine(command.deadLine.Time, command.deadLine.Date);


        if (command.imageIds != null)
        {
            task.Images = new List<TaskImage>();
            foreach (var item in command.imageIds)
            {
                task.Images.Add(new TaskImage
                {
                    ImageId = item,
                });
            }
        }

        try
        {

            await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {

            throw;
        }

        var created = await _taskCatalogDbContext.Tasks
                   .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

        var TaskDto = _mapper.Map<TaskDto>(created);

        _logger.LogInformation("Task a with ID: '{TaskId} created.'", command.Id);

        var result = new FluentResults.Result();

        return result.ToResult(new CreateTaskResult(TaskDto));
    }
}