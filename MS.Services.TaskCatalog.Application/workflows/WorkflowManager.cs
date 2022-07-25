using Ardalis.GuardClauses;
using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MS.Services.TaskCatalog.Application.Hubs;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Domain;
using MS.Services.TaskCatalog.Infrastructure;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.EnumBuilderExtensions;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.FcmExtentions;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.Dependency;
using MsftFramework.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MS.Services.TaskCatalog.Domain.Workflows
{

    //public enum WorkflowState
    //{
    //    Done, InProgress, NotStart
    //}
    //public enum WorkflowActionResult
    //{
    //    Done, InProgress, NotStart
    //}
    //class WorkflowStep
    //{
    //    private int step;

    //    public WorkflowStep(string name, int step, WorkflowStep nextStep, IList<WorkflowAgent> agents)
    //    {
    //        Name = name;
    //        this.step = step;
    //        NextStep = nextStep;
    //        this.Agents = agents;
    //    }

    //    public static WorkflowStep Create(string name, int step, WorkflowStep nextStep, IList<WorkflowAgent> agents)
    //    {
    //        return new WorkflowStep(name, step, nextStep, agents);
    //    }
    //    public WorkflowState State = WorkflowState.NotStart;
    //    public async void Exectute()
    //    {
    //        while (true)
    //        {

    //            if (CurrentAgent == null && State == WorkflowState.NotStart)
    //                CurrentAgent = Agents.FirstOrDefault(e => e.Order == 1);
    //            else
    //                CurrentAgent = CurrentAgent.GetNextAgent();

    //            if (CurrentAgent == null)
    //            {
    //                if (NextStep != null)
    //                    NextStep.Exectute();
    //                return;
    //            }

    //            State = WorkflowState.InProgress;
    //            await CurrentAgent.DoTask(this);


    //        }


    //    }
    //    public WorkflowAgent CurrentAgent { get; private set; }
    //    public IList<WorkflowAgent> Agents = new List<WorkflowAgent>();

    //    public string Name { get; }
    //    public int StepNumber { get; }
    //    public WorkflowStep NextStep { get; }

    //}
    //class WorkflowAgent
    //{
    //    public WorkflowAgent(string name, int order, WorkflowAgent nextAgent)
    //    {
    //        Name = name;
    //        Order = order;
    //        NextAgent = nextAgent;
    //    }

    //    public static WorkflowAgent Create(string name, int order, WorkflowAgent nextAgent)
    //    {
    //        return new WorkflowAgent(name, order, nextAgent);
    //    }

    //    public WorkflowAgent GetNextAgent() => NextAgent;


    //    public async Task<WorkflowActionResult> DoTask(WorkflowStep workflowStep)
    //    {
    //        Console.WriteLine($"Action execute in step {workflowStep.Name} with agent {this.Name}");
    //        WorkflowActionResult = WorkflowActionResult.Done;
    //        return await
    //       Task.FromResult(WorkflowActionResult.Done);

    //    }
    //    public WorkflowActionResult WorkflowActionResult = WorkflowActionResult.NotStart;
    //    public int Order { get; }
    //    public string Name { get; }
    //    WorkflowAgent NextAgent { get; }
    //}

    internal class WorkflowManager
    {

        public WorkflowManager()
        {
        }
        public void Init()
        {
            RecurringJob.AddOrUpdate("RecurringWorkflowManager", () => Start(), Cron.Minutely);
        }


        public static WorkflowManager Manager = new();

        public void Start()
        {

            var taskCatalogDbContext = ServiceActivator.GetScopeService<ITaskCatalogDbContext>();
            var wrkflows = taskCatalogDbContext.WorkflowInstance
                .Include(x => x.workflowSteps)
                .ThenInclude(x => x.Owner)
                .Include(w => w.workflowSteps)
                .ThenInclude(s => s.WorkflowRoleUsers)
                .ThenInclude(x => x.User)
                .Include(x => x.workflowSteps)
                .ThenInclude(x => x.WorkflowStepAlertInstances)
                .ThenInclude(x => x.WorkFlowAlert)
                .Include(x => x.workflowSteps)
                .ThenInclude(x => x.WorkflowRoleModel)
                .ThenInclude(x => x.Roles)
                .Where(e => e.Status == WorkflowStatus.InProgress).ToList();

            var inProgressWorkflows = wrkflows.Where(e => e.workflowSteps.Any(c =>
            c.Status != WorkflowStatus.Done && c.Status != WorkflowStatus.Stop
            && c.Status != WorkflowStatus.Fail
            && c.LastVisit >= DateTime.MinValue
            && DateTime.Now > c.LastVisit!.Add(c.DeadLine)
            )).ToList();
            inProgressWorkflows.ForEach(workflow =>
            {
                var nextStep = GetStep(workflow, taskCatalogDbContext);

                Execute(workflow, nextStep, taskCatalogDbContext);
            }
            );
        }
        private void Execute(WorkflowInstance workflow, WorkflowStepInstance workflowStep, ITaskCatalogDbContext taskCatalogDbContext)
        {
            if (workflowStep == null)
                return;
            var nextAgent = workflowStep.WorkflowRoleUsers.Where(e => e.Visited == false && DateTime.Now > e.WorkflowStepInstance.LastVisit.Add(e.WorkflowStepInstance.DeadLine)).OrderBy(o => o.Order).ThenBy(h => h.Visited).FirstOrDefault();

            if (nextAgent != null)
                CreateTask(workflowStep, nextAgent, taskCatalogDbContext);


        }
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private async void CreateTask(WorkflowStepInstance workflowStep, WorkflowRoleUser nextAgent, ITaskCatalogDbContext taskCatalogDbContext)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(10);
                //Creat Task for new agent
                string taskName = $"{workflowStep.Id}-{nextAgent.Id}";
                if (taskCatalogDbContext.Tasks.Any(e => e.Name == taskName))
                    return;

                var data = new CreateTaskRequest
                {
                    Name = taskName,
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now.Add(workflowStep.DeadLine),
                    Priority = priorityType.High,
                    ImportanceType = ImportanceType.ZMF,
                    UserIds = new long[] { nextAgent.UserId },
                    Description = $"Task Assig to {nextAgent.User.Name} :for step: {workflowStep.Name} in Time:{DateTime.Now.TimeOfDay}"
                };
                Console.WriteLine($"Task in Step:{workflowStep.Name} send to :{nextAgent.User.Name}");

                var mapper = ServiceActivator.GetRequiredService<IMapper>();
                var mediator = ServiceActivator.GetScopeService<IMediator>();
                var commandProcessor = ServiceActivator.GetScopeService<ICommandProcessor>();

                var command = mapper.Map<CreateTaskCommand>(data);
                var result = await mediator.Send(command);
                workflowStep.SetRelatedTask(result.Value.Task.Id);
                nextAgent.SetRelatedTaskTime();

                await taskCatalogDbContext.SaveChangesAsync();
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                semaphoreSlim.Release();
            }
        }



        private WorkflowStepInstance? GetStep(WorkflowInstance workflowInstance, ITaskCatalogDbContext taskCatalogDbContext)
        {
            var mustBeNotify = workflowInstance.workflowSteps.Where(x => x.WorkflowRoleUsers.Any(e => e.Visited == false && e.VisitTime > DateTime.MinValue && e.NotificationCount <= e.WorkflowStepAlertInstances.Count()
            && e.WorkflowStepInstance.Status == WorkflowStatus.InProgress
            )
            ).FirstOrDefault();
            if (mustBeNotify != null)
            {
                var lastUser = mustBeNotify.WorkflowRoleUsers.OrderByDescending(e => e.VisitTime).ThenBy(x => x.NotificationCount).FirstOrDefault();
                SendNotification(mustBeNotify, lastUser!);
                lastUser!.Visit();
                taskCatalogDbContext.SaveChanges();
                return null;
            }


            var next = workflowInstance.workflowSteps
                .Where(e => e.WorkflowRoleUsers.Any(e => e.Visited == false) && e.Status != WorkflowStatus.Done && e.Status != WorkflowStatus.Fail)
                .OrderBy(c => c.Id).ThenByDescending(x => x.Status)
                 .FirstOrDefault()!;
            var doneStep = workflowInstance.workflowSteps.Where(e => e.WorkflowRoleUsers.All(e => e.Visited == true) && e.Status != WorkflowStatus.Done && e.Status != WorkflowStatus.Fail).FirstOrDefault();

            if (next == null)
            {


                if (doneStep != null && doneStep.Status != WorkflowStatus.Done)
                    doneStep.ChangeStatus(WorkflowStatus.Fail);

                if (workflowInstance.workflowSteps.All(h => h.Status == WorkflowStatus.Done))
                    workflowInstance.ChangeStatus(WorkflowStatus.Done);
                else
                    workflowInstance.ChangeStatus(WorkflowStatus.Fail);

                taskCatalogDbContext.SaveChanges();

                var mapper = ServiceActivator.GetScopeService<IMapper>();
                var WorkflowDto = mapper.Map<List<WorkflowInstanceDto>>(workflowInstance);
                WorkflowInstanceHub.UpdateWorkflowInstanceById(WorkflowDto);
                Console.WriteLine($"Workflow fully Completed {workflowInstance.Name}");
                return null;
            }
            if (doneStep != null)
            {
                doneStep.ChangeStatus(WorkflowStatus.Fail);
                workflowInstance.ChangeStatus(WorkflowStatus.Stop);
                taskCatalogDbContext.SaveChanges();


                var mapper = ServiceActivator.GetScopeService<IMapper>();
                var WorkflowDto = mapper.Map<List<WorkflowInstanceDto>>(workflowInstance);
                WorkflowInstanceHub.UpdateWorkflowInstanceById(WorkflowDto);


                if (doneStep.Owner != null)
                    SendCustomNotification(doneStep.Owner.DeviceId, "اخطار", "رسیدگی به کارمندان");

                return null;
            }
            next.ChangeStatus(WorkflowStatus.InProgress);
            taskCatalogDbContext.SaveChanges();
            return next;
        }

        private async void SendCustomNotification(string deviceId, string title, string body)
        {

            var fcmMessaging = ServiceActivator.GetScopeService<IFcmMessaging>();
            //await fcmMessaging.SendAsync("fAZcc5SpTpqKa9BTlr0q_2:APA91bGa9HGI8ukFXEI6mVTfPLfyua38qbFDJTZxnFbVAk3JmH3nfwXV9rcf0K2XGIzfrxWKzCt--EvCXGX-W1w9OSCfOjmPMHKJMcadzg-hYr5JldvPetfuoVIYhrtGbi5bqvoFFJ_6", mustBeNotify.User.Name, alert.WorkFlowAlert.Body);
            var result = await fcmMessaging.SendAsync(deviceId, title, body);
            if (!result)
                Console.WriteLine("Notificaton Did not Send");

        }
        private async void SendNotification(WorkflowStepInstance stepInstance, WorkflowRoleUser mustBeNotify)
        {
            if (mustBeNotify.WorkflowStepAlertInstances.Any())
            {
                if (mustBeNotify.NotificationCount >= mustBeNotify.WorkflowStepAlertInstances.Count())
                    return;

                var alert = mustBeNotify.WorkflowStepAlertInstances.ToList()[mustBeNotify.NotificationCount];

                var fcmMessaging = ServiceActivator.GetScopeService<IFcmMessaging>();
                //await fcmMessaging.SendAsync("fAZcc5SpTpqKa9BTlr0q_2:APA91bGa9HGI8ukFXEI6mVTfPLfyua38qbFDJTZxnFbVAk3JmH3nfwXV9rcf0K2XGIzfrxWKzCt--EvCXGX-W1w9OSCfOjmPMHKJMcadzg-hYr5JldvPetfuoVIYhrtGbi5bqvoFFJ_6", mustBeNotify.User.Name, alert.WorkFlowAlert.Body);
                var result = await fcmMessaging.SendAsync(mustBeNotify.User.DeviceId, mustBeNotify.User.Name, alert.WorkFlowAlert.Body);
                if (!result)
                    Console.WriteLine("Notificaton was not Send");

                mustBeNotify.AddNotificationCount();
                stepInstance.SetDelay(alert.Delay);
            }
        }

        internal async void DoneTask(Tasks.Task task)
        {
            using var TaskCatalogDbContext = ServiceActivator.GetScopeService<ITaskCatalogDbContext>();


            var workflow = await TaskCatalogDbContext.WorkflowInstance
                .Include(x => x.workflowSteps)
                .ThenInclude(x => x.WorkflowRoleUsers)
                .Where(h => h.workflowSteps.Any(g => g.TaskId == task.Id)).FirstOrDefaultAsync();
            if (workflow != null && workflow.Status != WorkflowStatus.Done)
            {
                var currentState = workflow.workflowSteps.FirstOrDefault(e => e.TaskId == task.Id);
                if (currentState != null)
                    currentState.Complete();

                var step = GetStep(workflow, TaskCatalogDbContext);
                if (workflow != null)
                {
                    if (step != null)
                    {
                        workflow.ChangeStatus(WorkflowStatus.InProgress);
                        if (currentState!.Id != step.Id)
                            step.ChangeStatus(WorkflowStatus.InProgress);
                    }
                    else
                        workflow.ChangeStatus(WorkflowStatus.Done);

                    var mapper = ServiceActivator.GetScopeService<IMapper>();
                    var WorkflowDto = mapper.Map<List<WorkflowInstanceDto>>(workflow);
                    await WorkflowInstanceHub.UpdateWorkflowInstanceById(WorkflowDto);
                }
                await TaskCatalogDbContext.SaveChangesAsync();
            }
        }
    }
}