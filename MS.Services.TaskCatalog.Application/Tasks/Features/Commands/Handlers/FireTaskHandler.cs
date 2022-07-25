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
using Hangfire;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Commands.Handlers;
public class FireTaskHandler : ICommandHandler<FireTaskCommand, bool>, IDisposable
{
    private readonly ILogger<CreateTaskHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    public FireTaskHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateTaskHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<bool>> Handle(
        FireTaskCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        RecurringJob.AddOrUpdate("ReportTask", () => StartReportAsync(), Cron.Daily(6, 0));

        RecurringJob.AddOrUpdate("Return_Task_Every_Night", () => Check_Return_Task_EveryNight(), Cron.Daily(8, 0));
        
        var result = new Result();
        return result.ToResult(true);
    }
    public void Check_Return_Task_EveryNight()
    {
        var unDoneTasks = _taskCatalogDbContext.Tasks
            .Where(x => x.Status == Domain.SharedKernel.TaskStatus.UnDone)
            .Where(x => x.StartDateTime.Date == DateTime.Now.Date)
            .ToList();

        unDoneTasks.ForEach(x =>
        {
            if (DateTime.Now > x.EndDateTime)
                x.SetReturnCount((x.EndDateTime - DateTime.Now).Days);
            else
                x.SetReturnCount((DateTime.Now - x.StartDateTime).Days);
            x.ChangeStatus(null);
        });
        _taskCatalogDbContext.SaveChanges();

    }
    public void StartReportAsync()
    {
        var tasks = _taskCatalogDbContext.Tasks.Include(h => h.TaskProgresses).Where(x => x.StartDateTime.Date == DateTime.Now.Date && x.IsTodayTask).ToList();
        tasks.ForEach(x =>
            {
                var controllerprogress = x.TaskProgresses!.FirstOrDefault(h => h.UserRole == Domain.SharedKernel.UserRoleType.Controller)?.Progress ?? 0;
                var userprogress = x.TaskProgresses!.FirstOrDefault(h => h.UserRole == Domain.SharedKernel.UserRoleType.User)?.Progress ?? 0;

                if (controllerprogress == 100 && userprogress == 100)
                {
                    x.ChangeStatus(Domain.SharedKernel.TaskStatus.Done);

                    x.AddDomainEvent(new TaskDoneEvent(x));
                }
                if (controllerprogress == 50 && userprogress == 100)
                {
                    x.ChangeStatus(Domain.SharedKernel.TaskStatus.UnCompleted);
                    x.SetTodayTask(false);
                }
                if (controllerprogress == 0 && userprogress == 0)
                {
                    x.ChangeStatus(Domain.SharedKernel.TaskStatus.UnDone);
                    x.SetTodayTask(false);
                }


            });
        _taskCatalogDbContext.SaveChanges();

    }
    public void Dispose()
    {
        _taskCatalogDbContext.Dispose();
    }
}