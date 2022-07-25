using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Workflows;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Domain.workflows
{
    public class WorkflowRoleUser : Entity<long>
    {
        public WorkflowRoleUser(long id , long roleId, long userId, int order, long workflowStepInstanceId)
        {
            Id = id;
            RoleId = roleId;
            UserId = userId;
            Order = order;
            WorkflowStepInstanceId = workflowStepInstanceId; 
        }

        public long WorkflowStepInstanceId { get; private set; }
        public WorkflowStepInstance WorkflowStepInstance { get; private set; }
        public long RoleId { get;  set; }
        public Role Role { get;  set; }
        public long UserId { get; private set; }
        public User User { get; private set; }
        public bool Visited { get; private set; }
        public int NotificationCount { get; private set; }
        public int Order { get; private set; }
        public DateTime VisitTime { get; set; }
        public ICollection<WorkflowStepAlertInstance> WorkflowStepAlertInstances { get; set; }
        public void AddNotificationCount()
        {
            NotificationCount = NotificationCount + 1;
        }
        public void Visit()
        {
            if (NotificationCount == WorkflowStepAlertInstances.Count())
            {
                this.Visited = true;
            }
            VisitTime = DateTime.Now;
        }
        public void SetRelatedTaskTime() => VisitTime = DateTime.Now;

        public static WorkflowRoleUser Create(long id,long roleId, long userId, int order, long workflowStepInstanceId)
        {
            return new WorkflowRoleUser(id,roleId, userId, order, workflowStepInstanceId);
        }
    }
}
