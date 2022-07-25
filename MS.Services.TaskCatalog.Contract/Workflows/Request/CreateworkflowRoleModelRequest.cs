using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Domain.Workflows.Models;

namespace MS.Services.TaskCatalog.Contract.Workflows.Request;


public record CreateworkflowRoleModelRequest
{
    public string Name { get; init; } = default!;
    public long UnitId { get; set; }
    public List<RolesDto> Roles { get; set; }

}

