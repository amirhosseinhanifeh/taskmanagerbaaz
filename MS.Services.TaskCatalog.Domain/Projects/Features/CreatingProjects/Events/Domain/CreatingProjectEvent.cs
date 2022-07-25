
using MS.Services.TaskCatalog.Domain.Projects.ValueObjects;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Domain;
public record CreatingProjectEvent(
    ProjectId Id,
    ProjectName Name,
    string? Description = null) : DomainEvent;
