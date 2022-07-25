using MsftFramework.Core.Domain.Events.External;

namespace MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Integration;

public record WorkflowManagerCreatedIEvent(
            string workflowName,
            long chainNumber) :
   IntegrationEvent;
