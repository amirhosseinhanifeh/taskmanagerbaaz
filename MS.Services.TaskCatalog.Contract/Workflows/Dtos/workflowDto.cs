using MS.Services.TaskCatalog.Domain.Workflows;

namespace MS.Services.TaskCatalog.Contract.Workflows.Dtos;
public record WorkflowDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public int StepsCount { get; set; }
    public ICollection<WorkflowStepsDto> Steps { get; set; }

}
public record WorkflowsDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public int StepsCount { get; set; }
    public ICollection<WorkflowStepsDto> Steps { get; set; }

}
public record WorkflowInstanceDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public int StepsCount { get; set; }
    public string StatusTxt { get; set; }
    public WorkflowStatus Status { get; set; }
    public List<WorkflowInstaceStepsDto> Steps { get; set; }
}


