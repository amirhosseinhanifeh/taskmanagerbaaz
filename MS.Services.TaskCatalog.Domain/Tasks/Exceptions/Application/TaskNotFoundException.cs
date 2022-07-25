using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Application;

public class TaskNotFoundException : NotFoundException
{
    public TaskNotFoundException(string message) : base(message)
    {
    }
    public TaskNotFoundException(long id) : base($"Task with id '{id}' not found") { }
}