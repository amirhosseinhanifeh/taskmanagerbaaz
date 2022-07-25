namespace MS.Services.TaskCatalog.Contract.Workflows.Request;

public record CreateWorkflowStepsRequest
{
    public string Name { get; init; } = default!;
    public long   workflowId { get; init; } 
}

public record CreateAlertRequest
{
    public string Body { get; set; }
}