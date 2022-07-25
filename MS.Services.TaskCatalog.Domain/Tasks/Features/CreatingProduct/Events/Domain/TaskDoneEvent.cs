using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;

public record TaskDoneEvent(Task Task) : DomainEvent;
