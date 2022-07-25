using MsftFramework.Core.Domain.Exceptions;

namespace MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Domain;
public class WorkflowDomainException : DomainException
{
    public WorkflowDomainException(string message) : base(message) { }
}
