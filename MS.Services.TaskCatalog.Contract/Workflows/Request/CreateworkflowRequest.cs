using MS.Services.TaskCatalog.Domain.Workflows.Models;

namespace MS.Services.TaskCatalog.Contract.Workflows.Request;


public record CreateWorkflowRequest
{
    public string Name { get; init; } = default!;
    public IList<WorkflowStepRequest> WorkflowSteps { get; set; }


}
public record InitWorkflowRequest
{
    public string Name { get; init; } = default!;
    public long  WorkflowId { get; set; }
    public long? workflowInstanceId { get; set; }
    public long workflowStepId { get; set; }
    public IList<RoleMappingRequest> Roles { get; set; }
    public string Description { get; set; }


}
public record RoleMappingRequest
{
    public long RoleId { get; init; } = default!;
    public long UserId { get; set; }
    public long WorkflowStepId { get; set; }
    public List<AssingAlertToRoleRequest> Alerts { get; set; }
}
public record StartWorkflowRequest
{
    public long WorkflowInstanceId { get; set; }


}


public record WorkflowStepRequest
{
    public string Name { get; set; }
    public int Order { get; set; }
    public TimeSpan Deadline { get; set; }
    public long WorkflowRoleModelId { get; set; }
}

public record WorkflowAlertRequest
{
    public int Delay { get; set; }
    public long WorkflowAlertId { get; set; }
}

public record AssingAlertToRoleRequest
{
    public long AlertId { get; set; }
    public int Delay { get; set; }
    public int Order { get; set; }
}