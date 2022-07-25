using MsftFramework.Core.Domain.Exceptions;

namespace MS.Services.TaskCatalog.Domain.Comments.Exceptions.Domain;
public class CommentDomainException : DomainException
{
    public CommentDomainException(string message) : base(message) { }
}