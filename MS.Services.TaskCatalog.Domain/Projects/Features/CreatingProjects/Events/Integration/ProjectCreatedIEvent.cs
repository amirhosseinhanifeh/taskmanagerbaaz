using MsftFramework.Core.Domain.Events.External;

namespace MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Integration;
public record ProjectCreatedIEvent(long Id, string Name) :
   IntegrationEvent;