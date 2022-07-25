namespace MS.Services.TaskCatalog.Contract.Workflows.Dtos;

public record WorkflowStepsDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public string DeadLine { get; set; }
    public RoleModelDto RoleModel { get; set; }
}

public record WorkflowInstaceStepsDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public string StatusTxt { get; set; }
    public int Status { get; set; }
    public bool IsDone { get; set; }
    public bool IsProgress { get; set; }
    public bool IsFailed { get; set; }
    public bool IsPending { get; set; }
}