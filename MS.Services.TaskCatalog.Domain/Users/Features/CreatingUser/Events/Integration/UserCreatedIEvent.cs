using MsftFramework.Core.Domain.Events.External;

namespace MS.Services.TaskCatalog.Domain.Users.Features.CreatingUser.Events.Integration;
public record UserCreatedIEvent(long Id, string Name) :
   IntegrationEvent;