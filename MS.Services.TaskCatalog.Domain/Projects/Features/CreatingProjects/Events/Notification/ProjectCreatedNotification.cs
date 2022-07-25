   using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Domain;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProjects.Events.Domain;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Notification;

public record ProjectCreatedNotification(ProjectCreatedEvent DomainEvent) : DomainNotificationEventWrapper<ProjectCreatedEvent>(DomainEvent);