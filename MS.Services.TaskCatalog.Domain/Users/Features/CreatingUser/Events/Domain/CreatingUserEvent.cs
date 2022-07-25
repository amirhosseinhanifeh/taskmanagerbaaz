using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Users.Features.CreatingUser.Events.Domain;
public record CreatingUserEvent(
    UserId Id,
    long Name,
    string? Description = null) : DomainEvent;