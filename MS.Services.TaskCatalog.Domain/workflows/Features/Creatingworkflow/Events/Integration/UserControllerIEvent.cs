using MsftFramework.Core.Domain.Events.External;

namespace MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Integration;

public record UserControllerIEvent(
 long id,
 string name,
 int previousReceiver,
 int nextReceiver
) :
IntegrationEvent;
