using MsftFramework.Core.Domain.Exceptions;

namespace MS.Services.TaskCatalog.Domain.Projects.Exceptions.Domain;
public class ProjectDomainException : DomainException
{
    public ProjectDomainException(string message) : base(message) { }
}