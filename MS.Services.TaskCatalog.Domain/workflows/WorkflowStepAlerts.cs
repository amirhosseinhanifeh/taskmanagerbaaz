using MS.Services.TaskCatalog.Domain.Workflows;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Domain.workflows
{
    public class WorkflowStepAlerts : Entity<long>
    {
        public int Order { get; private set; }
        public int Delay { get; private set; }
        public long RoleId { get; internal set; }
        public Role Role { get; private set; }

        public long WorkflowAlertId { get; set; }
        public WorkFlowAlerts WorkFlowAlert { get; set; }

        public static WorkflowStepAlerts Create(
            int order,
            int delay,
            long workflowalertId)
        {



            return new WorkflowStepAlerts
            {
                Order = order,
                Delay = delay,
                WorkflowAlertId = workflowalertId,
            };
        }

        private void SendNotification(long userId)
        {

        }
    }

    public class WorkflowStepAlertInstance : Entity<long>
    {
        public int Order { get; set; }
        public int Delay { get; set; }


        public long WorkflowAlertId { get; set; }
        public WorkFlowAlerts WorkFlowAlert { get; set; }

        public long WorkflowRoleUserId { get; set; }
        public WorkflowRoleUser WorkflowRoleUser { get; set; }

        public long WorkflowStepInstanceId { get; set; }
        public WorkflowStepInstance WorkflowStepInstance { get; set; }
        public static WorkflowStepAlertInstance Create(
            long id,
            int order,
            int delay,
            long workflowalertId,
            long workflowStepInstanceId)
        {

            return new WorkflowStepAlertInstance
            {

                Id=id,
                Order = order,
                Delay = delay,
                WorkflowAlertId = workflowalertId,
                WorkflowStepInstanceId=workflowStepInstanceId
            };
        }

        private void SendNotification(long userId)
        {

        }
    }
}
