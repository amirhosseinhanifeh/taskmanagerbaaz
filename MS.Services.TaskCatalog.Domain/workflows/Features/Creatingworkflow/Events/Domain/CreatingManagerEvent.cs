using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Workflows.Features.CreatingWorkflow.Events.Domain;

public record CreatingManagerEvent(
           long Id,
           string UserId,
           string UserName) : DomainEvent;
 