using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Commands;

public record CreatetWorkflowRoleModelCommand(string Name,long UnitId,IList<RolesDto> Roles) : ITxCreateCommand<FluentResults.Result<CreateRoleModelResult>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();

}


