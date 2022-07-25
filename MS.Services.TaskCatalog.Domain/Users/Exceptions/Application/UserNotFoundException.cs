using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Users.Exceptions.Application;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
    public UserNotFoundException(long id) : base($"User with id '{id}' not found") { }
}