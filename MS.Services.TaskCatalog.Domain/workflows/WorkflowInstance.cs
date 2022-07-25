using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Domain.Workflows.ValueObjects;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Domain.Workflows
{
    public class WorkflowInstance : Entity<long>
    {
        public string Name { get; private set; }
        public string Description { get; private set; } = default!;
        public WorkflowStatus Status { get; private set; }
        public WorkflowId WorkflowId { get; private set; }
        public Workflow Workflow { get; private set; } = default!;
        public List<WorkflowStepInstance> workflowSteps { get; private set; } = new();

        public static WorkflowInstance Create(Workflow? workflow, WorkflowInstance? workflowInstance, string name, string description, long workflowstepId, IList<WorkflowUserRole> roles)
        {
            if (workflowInstance == null)
            {
                workflowInstance = new WorkflowInstance() { Id = SnowFlakIdGenerator.NewId(), Name = name, WorkflowId = workflow.Id, Description = description,Status=WorkflowStatus.Pending };

                var step = workflow.WorkflowSteps.FirstOrDefault(x => x.Id == workflowstepId);


                var newStep = WorkflowStepInstance.Create(workflowInstance.Id, step.Name, step.DeadLine, step.Order,WorkflowStatus.Pending);

                newStep.AddAgents(step.WorkflowRoleModel, roles, newStep.Id);

                workflowInstance.workflowSteps.Add(newStep);
            }
            else
            {
                var newStep = workflow.WorkflowSteps.FirstOrDefault(x => x.Id == workflowstepId);
                if (newStep == null)
                    return null;
                return Update(workflowInstance, newStep!, roles);
            }

            return workflowInstance;
        }
        public static WorkflowInstance Update(WorkflowInstance workflowInstance, WorkflowStep workflowStep, IList<WorkflowUserRole> roles)
        {
            var newStep = WorkflowStepInstance.Create(workflowInstance.Id, workflowStep.Name, workflowStep.DeadLine, workflowStep.Order, WorkflowStatus.Pending);

            newStep.AddAgents(workflowStep.WorkflowRoleModel, roles, newStep.Id);

            workflowInstance.workflowSteps.Add(newStep);

            return workflowInstance;
        }
        public void ChangeStatus(WorkflowStatus status)
        {
            this.Status = status;
            var lastState = this.workflowSteps.Where(e => e.Status != WorkflowStatus.Fail).OrderBy(e => e.Order).ThenByDescending(e => e.LastVisit).FirstOrDefault();
            if (lastState == null)
                return;

            if (lastState.Status != WorkflowStatus.Done && status == WorkflowStatus.InProgress)
                lastState.ChangeStatus(WorkflowStatus.InProgress);

            lastState.LastVisit = DateTime.Now;
        }
    }
    public class WorkflowUserRole
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public long WorkflowStepId { get; set; }
        public List<AssingAlertToRole> Alerts { get; set; }
    }
    public record AssingAlertToRole
    {
        public long RoleId { get; set; }
        public long AlertId { get; set; }
        public int Delay { get; set; }
        public int Order { get; set; }
    }
}
