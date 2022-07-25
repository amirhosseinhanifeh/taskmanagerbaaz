using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;

public record CreatingUserControllerEvent(
    long id,
    string name,
    int previousReceiver,
    int nextReceiver
    ) : DomainEvent;