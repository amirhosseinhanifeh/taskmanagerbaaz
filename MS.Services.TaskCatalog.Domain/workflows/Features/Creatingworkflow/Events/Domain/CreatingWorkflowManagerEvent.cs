using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;

public record CreatingWorkflowManagerEvent(
            long id,
            string name
    ) : DomainEvent;

 