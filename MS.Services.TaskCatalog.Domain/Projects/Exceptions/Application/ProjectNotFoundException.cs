using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Projects.Exceptions.Application;

public class ProjectNotFoundException : NotFoundException
{
    public ProjectNotFoundException(string message) : base(message)
    {
    }
    public ProjectNotFoundException(long id) : base($"Project with id '{id}' not found") { }
}