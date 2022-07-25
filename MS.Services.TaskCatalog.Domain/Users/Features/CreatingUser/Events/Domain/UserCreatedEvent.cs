using MS.Services.TaskCatalog.Domain.Tasks;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Users.Features.CreatingUser.Events.Domain;
public record UserCreatedEvent(User User) : DomainEvent;