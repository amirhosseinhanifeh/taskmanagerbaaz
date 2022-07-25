using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Workflows.Models
{
    public record WorkflowStepDto
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public TimeSpan Deadline { get; set; }
        public long WorkflowRoleModelId { get; set; }
        //public IList<WorkflowStepAlertDto> WorkflowStepAlerts { get; set; }
    }
    public record WorkflowStepAlertDto
    {
        public int Order { get; set; }
        public int Delay { get; set; }
        public long WorkflowAlertId { get; set; }


    }
}
