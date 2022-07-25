using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Infrastructure;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.FcmExtentions;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.Dependency;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Commands.Handlers;
public class CreateTaskNotifHandler : ICommandHandler<AddTaskNotifCommand, bool>, IDisposable
{
    private readonly ILogger<CreateTaskNotifHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

    public CreateTaskNotifHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateTaskNotifHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<bool>> Handle(
        AddTaskNotifCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        BackgroundJob.Schedule(() => SendNotif(command.userId, command.taskId, cancellationToken), TimeSpan.FromMinutes(command.time.TotalMinutes));

        _logger.LogInformation("Task a with ID: '{TaskId} created.'", command.Id);

        var result = new Result();
        return result.ToResult(true);
    }

    public void SendNotif(long? userId, long taskId, CancellationToken cancellationToken)
    {
        var fcmMessaging = ServiceActivator.GetScopeService<IFcmMessaging>();
        //await fcmMessaging.SendAsync("fAZcc5SpTpqKa9BTlr0q_2:APA91bGa9HGI8ukFXEI6mVTfPLfyua38qbFDJTZxnFbVAk3JmH3nfwXV9rcf0K2XGIzfrxWKzCt--EvCXGX-W1w9OSCfOjmPMHKJMcadzg-hYr5JldvPetfuoVIYhrtGbi5bqvoFFJ_6", mustBeNotify.User.Name, alert.WorkFlowAlert.Body);

        var user = _taskCatalogDbContext.Users.Find(userId);
        if (user != null)
        {
            var result = fcmMessaging.SendAsync(user.DeviceId, "هشدار", $"درخواست ثبت درصد پیشرفت برای تسک {taskId}").Result;
            if (!result)
                Console.WriteLine("Notificaton Did not Send");

            _taskCatalogDbContext.TaskNotifications.Add(new TaskNotification
            {
                Title = $"درخواست ثبت درصد پیشرفت برای تسک {taskId}",
                UserId = userId,
                TaskId = taskId,
            });
            _taskCatalogDbContext.SaveChanges();
        }
    }
    public void Dispose()
    {
        _taskCatalogDbContext.Dispose();
    }
}