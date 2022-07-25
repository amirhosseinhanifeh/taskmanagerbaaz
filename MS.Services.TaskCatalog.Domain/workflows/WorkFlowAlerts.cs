using MsftFramework.Core.Domain.Model;

namespace MS.Services.TaskCatalog.Domain.workflows
{
    public class WorkFlowAlerts:Entity<long>
    {
        public string Body { get; set; }

        public ICollection<WorkflowStepAlerts> WorkflowStepAlerts { get; set; }

        public static WorkFlowAlerts Create(
            long id,
            string body)
        {
            return new WorkFlowAlerts
            {
                Id=id,
                Body = body
            };
        }

    }
}
