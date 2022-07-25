using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;

public class UserControllerNotFoundException : NotFoundException
{
    public UserControllerNotFoundException(string message) : base(message)
    {
    }
    public UserControllerNotFoundException() : base($"UserController  not found") { }
}

public class UserControllerByIdNotFoundException : NotFoundException
{
    public UserControllerByIdNotFoundException(string message) : base(message)
    {
    }
    public UserControllerByIdNotFoundException(long id) : base($"UserController with id '{id}' not found") { }
}
