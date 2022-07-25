using MsftFramework.Core.Domain.Events.External;

namespace MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Integration;
public record TaskCreatedIEvent(long Id, string Name) :
   IntegrationEvent;