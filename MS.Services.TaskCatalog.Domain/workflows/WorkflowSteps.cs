using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
using MS.Services.TaskCatalog.Domain.Workflows.ValueObjects;
using MsftFramework.Core.Domain.Events.Internal;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.IdsGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Workflows
{
    public enum WorkflowStatus
    {
        [Display(Name ="در حالت انتظار")]
        Pending = 1,
        [Display(Name = "در حالت انجام کار")]
        InProgress = 2,
        [Display(Name = "انجام شده")]
        Done = 3,
        [Display(Name = "متوقف شده")]
        Stop = 4,
        [Display(Name = "مردود شده")]
        Fail = 5
    }
    public class WorkflowStep : Entity<long>
    {
        public int Order { get; set; }
        public string Name { get; private set; } = default!;
        public WorkflowId WorkFlowId { get; private set; }
        public Workflow Workflow { get; private set; }
        //public List<WorkflowAgent> WorkflowAgents { get; private set; } = new ();
        public TimeSpan DeadLine { get; private set; }
        public long WorkflowRoleModelId { get; private set; }
        public WorkflowRoleModel WorkflowRoleModel { get;private set; }
        public static WorkflowStep Create(
            string name,
            long workFlowId,
            TimeSpan deadline,
            int order,
            long workflowRoleModelId
            )
        {
            var Id = SnowFlakIdGenerator.NewId();
            var workflowManager = new WorkflowStep()
            {
                Id = Id,
                Name = name,
                WorkFlowId = workFlowId,
                DeadLine = deadline,
                Order = order,
                WorkflowRoleModelId = workflowRoleModelId,
            };
            return workflowManager;
        }

       
       
    }
}
