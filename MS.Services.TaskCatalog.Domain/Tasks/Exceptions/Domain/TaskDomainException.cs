using MsftFramework.Core.Domain.Exceptions;

namespace MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Domain;
public class TaskDomainException : DomainException
{
    public TaskDomainException(string message) : base(message) { }

}