using FluentResults;
using MediatR;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Commands;

public record ResumeWorkflowCommand(long workflowInstanceId) :
     ITxCreateCommand<FluentResults.Result<Unit>>
  {

    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}