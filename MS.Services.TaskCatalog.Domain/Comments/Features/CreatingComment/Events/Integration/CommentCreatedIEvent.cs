using MsftFramework.Core.Domain.Events.External;

namespace MS.Services.TaskCatalog.Domain.Comments.Features.CreatingComment.Events.Integration;
public record CommentCreatedIEvent(long Id, string Name) :
   IntegrationEvent;