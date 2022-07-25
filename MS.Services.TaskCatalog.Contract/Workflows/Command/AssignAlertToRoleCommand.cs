using MS.Services.TaskCatalog.Domain.Workflows.Models;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Commands;

public record AssignAlertToRoleCommand(
           WorkflowStepAlertDto[] model) : ITxCreateCommand<FluentResults.Result<bool>>
{
    public long RoleId { get; set; }
}