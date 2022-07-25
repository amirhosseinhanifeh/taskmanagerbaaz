using MsftFramework.Core.Domain.Exceptions;

namespace MS.Services.TaskCatalog.Domain.Users.Exceptions.Domain;
public class UserDomainException : DomainException
{
    public UserDomainException(string message) : base(message) { }
}