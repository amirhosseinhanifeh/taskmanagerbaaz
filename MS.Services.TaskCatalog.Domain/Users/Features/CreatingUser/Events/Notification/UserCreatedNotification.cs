
using MS.Services.TaskCatalog.Domain.Users.Features.CreatingUser.Events.Domain;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Users.Features.CreatingUser.Events.Notification;

public record UserCreatedNotification(UserCreatedEvent DomainEvent) : DomainNotificationEventWrapper<UserCreatedEvent>(DomainEvent);