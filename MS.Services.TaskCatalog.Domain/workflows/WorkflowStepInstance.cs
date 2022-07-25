using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Domain.workflows;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Domain.Workflows
{
    public class WorkflowStepInstance : Entity<long>
    {
        public string Name { get; private set; }
        public long WorkflowInstanceId { get; private set; }
        public WorkflowInstance WorkflowInstance { get; private set; }
        public TaskId? TaskId { get; private set; }
        public DateTime LastVisit { get; set; }
        public ICollection<WorkflowRoleUser> WorkflowRoleUsers { get; private set; }
        public WorkflowStatus Status { get; private set; }
        public TimeSpan DeadLine { get; private set; }
        public int Order { get; private set; }
        public long? OwnerId { get; private set; }
        public User Owner { get; private set; }
        public void Complete() => this.Status = WorkflowStatus.Done;

        public long? WorkflowRoleModelId { get; set; }

        public WorkflowRoleModel WorkflowRoleModel { get; set; }
        public ICollection<WorkflowStepAlertInstance> WorkflowStepAlertInstances { get; set; }
        public void SetRelatedTask(long value)
        {

            TaskId = value;
            LastVisit = DateTime.Now;

        }
        public WorkflowStepInstance()
        {

        }
        private WorkflowStepInstance(long workflowInstanceId, string name, TimeSpan deadline, int order,WorkflowStatus status)
        {
            Id = SnowFlakIdGenerator.NewId();
            Name = name;
            WorkflowInstanceId = workflowInstanceId;
            DeadLine = deadline;
            Order = order;
            Status = status;
        }


        public static WorkflowStepInstance Create(long workflowInstanceId, string name, TimeSpan deadline, int order,WorkflowStatus status)
        {
            return new WorkflowStepInstance(workflowInstanceId, name, deadline, order,status);
        }

        internal void AddAgents(WorkflowRoleModel roleModel, IList<WorkflowUserRole> roles, long workflowStepInstanceId)
        {
            this.WorkflowRoleModelId = roleModel.Id;
            this.WorkflowRoleUsers = new List<WorkflowRoleUser>();
            int order = 1;
            foreach (var item in roleModel.Roles.OrderBy(e => e.Id))
            {
                var role = roles.FirstOrDefault(e => e.RoleId == item.Id);
                if (role != null)
                {
                    var data = WorkflowRoleUser.Create(SnowFlakIdGenerator.NewId(), item.Id, role!.UserId, order++, workflowStepInstanceId);
                    data.WorkflowStepAlertInstances = new List<WorkflowStepAlertInstance>();

                    if (role != null)
                    {
                        foreach (var item2 in role.Alerts)
                        {
                            data.WorkflowStepAlertInstances.Add(WorkflowStepAlertInstance.Create(SnowFlakIdGenerator.NewId(), item2.Order, item2.Delay, item2.AlertId, workflowStepInstanceId));
                        }
                    }
                    this.WorkflowRoleUsers.Add(data);
                }
            }

        }

        public void ChangeStatus(WorkflowStatus workflowStatus)
        {
            this.Status = workflowStatus;
        }

        public void SetDelay(int delay)
        {
            this.DeadLine = TimeSpan.FromMinutes(delay);
            LastVisit = DateTime.Now;
        }
    }
}
