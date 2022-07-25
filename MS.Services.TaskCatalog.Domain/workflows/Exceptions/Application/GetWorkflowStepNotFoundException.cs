using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;

public class GetWorkflowStepNotFoundException : NotFoundException
{
    public GetWorkflowStepNotFoundException(string message) : base(message)
    {
    }
    public GetWorkflowStepNotFoundException() : base($"WorkflowStep not found") { }
}

public class GetWorkflowStepByIdNotFoundException : NotFoundException
{
    public GetWorkflowStepByIdNotFoundException(string message) : base(message)
    {
    }
    public GetWorkflowStepByIdNotFoundException(long id) : base($"WorkflowStep with id '{id}' not found") { }
}