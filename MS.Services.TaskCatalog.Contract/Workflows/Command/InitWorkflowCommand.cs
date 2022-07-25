using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Commands;

public record InitWorkflowCommand(string name,long workflowId,long? workflowInstanceId, string description ,long workflowstepId, IList<RoleMappingRequest> roles) : ITxCreateCommand<FluentResults.Result<InitWorkflowResult>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();

}


