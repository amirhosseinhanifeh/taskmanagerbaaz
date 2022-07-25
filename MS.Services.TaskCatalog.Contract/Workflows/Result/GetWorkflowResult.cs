using MS.Services.TaskCatalog.Contract.workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
namespace MS.Services.TaskCatalog.Contract.Workflows.Result;

public record GetWorkflowResult(IList<WorkflowsDto> Workflows);
public record GetWorkflowInstanceResult(IList<WorkflowInstanceDto> WorkflowInstances);
public record GetWorkflowInstancebyIdResult(WorkflowInstanceDto WorkflowInstance);
public record GetWorkflowByIdResult(WorkflowDto Workflow);
public record GetWorkflowRoleModelResult(IList<RoleModelDto> roleModels);

