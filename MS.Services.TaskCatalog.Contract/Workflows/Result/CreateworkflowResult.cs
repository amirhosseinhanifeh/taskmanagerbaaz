using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
namespace MS.Services.TaskCatalog.Contract.Workflows.Result;

public record CreateWorkflowResult(WorkflowDto WorkflowDto);
public record InitWorkflowResult(WorkflowInstanceDto WorkflowDto);
public record StartWorkflowResult(string step,string agentUser,string deadline);
public record CreateRoleModelResult(RoleModelDto RoleModels);
