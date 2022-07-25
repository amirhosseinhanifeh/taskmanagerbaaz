using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;

public class WorkflowsNotFoundException : NotFoundException
{
    public WorkflowsNotFoundException(string message) : base(message)
    {
    }
    public WorkflowsNotFoundException() : base($"Workflows with not found") { }
}


public class WorkflowsByIdNotFoundException : NotFoundException
{
    public WorkflowsByIdNotFoundException(string message) : base(message)
    {
    }
    public WorkflowsByIdNotFoundException(long id) : base($"Workflows with with id '{id}' not found") { }
}
