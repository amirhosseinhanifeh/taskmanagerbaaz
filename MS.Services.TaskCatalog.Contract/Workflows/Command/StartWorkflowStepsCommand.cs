using FluentResults;
using MediatR;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Commands;

public record StartWorkflowCommand(long workflowInstanceId) :
     ITxCreateCommand<FluentResults.Result<GetWorkflowInstancebyIdResult>>
  {

    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}