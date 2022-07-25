using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
public record CreatingTaskEvent(
    TaskId Id,
    TaskName Name,
    string? Description = null) : DomainEvent;
