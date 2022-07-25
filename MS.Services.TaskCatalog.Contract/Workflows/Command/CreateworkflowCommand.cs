using MediatR;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Domain.Workflows.Models;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Commands;

public record CreateWorkflowCommand(
           string name  , WorkflowStepDto[] WorkflowStepDto  ) : ITxCreateCommand<FluentResults.Result<Unit>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}
