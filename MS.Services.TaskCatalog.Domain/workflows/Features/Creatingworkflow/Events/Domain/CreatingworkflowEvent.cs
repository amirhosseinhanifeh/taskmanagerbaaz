using MS.Services.TaskCatalog.Domain.Workflows.ValueObjects;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;
public record CreatingWorkflowEvent(
           WorkflowId Id,
           WorkflowName Name
           ) : DomainEvent;
