
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProjects.Events.Domain;
public record ProjectCreatedEvent(Project Project) : DomainEvent;