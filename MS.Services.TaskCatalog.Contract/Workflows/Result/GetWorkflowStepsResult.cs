using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
namespace MS.Services.TaskCatalog.Contract.Workflows.Result;

public record GetWorkflowStepsResult(IList<WorkflowStepsDto> WorkflowStepsDtos);

