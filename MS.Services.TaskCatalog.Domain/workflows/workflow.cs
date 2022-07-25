using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Domain;
using MS.Services.TaskCatalog.Domain.Workflows.ValueObjects;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.Exception;
using MsftFramework.Core.Domain.Events.Internal;
using MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Domain.Workflows.Models;
using MsftFramework.Core.IdsGenerator;
using MS.Services.TaskCatalog.Domain.workflows;

namespace MS.Services.TaskCatalog.Domain.Workflows
{


    public class Workflow : Aggregate<WorkflowId>
    {

        public WorkflowName Name { get; private set; }
        public List<WorkflowStep> WorkflowSteps { get; private set; } = new();
        public List<WorkflowInstance> WorkflowInstances { get; private set; } = new();

        public static Workflow Create(
           WorkflowId id,
           WorkflowName name
           )
        {
            var Workflow = new Workflow
            {
                Id = Guard.Against.Null(id, new WorkflowDomainException("Workflow id can not be null")),
                Name = name

            };

            Workflow.ChangeName(name);

            Workflow.AddDomainEvent(new WorkflowCreatedEvent(Workflow));

            return Workflow;
        }
        /// <summary>
        /// Sets Workflow item name.
        /// </summary>
        /// <param name="name">The name to be changed.</param>
        public void ChangeName(WorkflowName name)
        {
            Guard.Against.Null(name, new WorkflowDomainException("Workflow name cannot be null."));

            Name = name;

        }

        public void CreateStep(WorkflowStepDto stepDto)
        {
            Guard.Against.Zero(stepDto.Order, "Order Must be greeter than zero");
            var newStep = WorkflowStep.Create(stepDto.Name, this.Id, stepDto.Deadline, stepDto.Order, stepDto.WorkflowRoleModelId);
            WorkflowSteps.Add(newStep);
        }
    }
}