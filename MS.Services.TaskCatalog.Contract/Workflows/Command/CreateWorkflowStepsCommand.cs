using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Commands;

public record CreateWorkflowStepsCommand(
           string name,
           long workflowId
           ) : ITxCreateCommand<FluentResults.Result<CreateWorkflowStepsResult>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
    public TimeSpan Deadline { get; set; }
}
public record CreateAlertCommand(
           string body
           ) : ITxCreateCommand<FluentResults.Result<bool>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}
