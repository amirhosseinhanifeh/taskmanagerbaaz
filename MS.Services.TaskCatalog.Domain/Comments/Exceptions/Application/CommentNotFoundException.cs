using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Comments.Exceptions.Application;

public class CommentNotFoundException : NotFoundException
{
    public CommentNotFoundException(string message) : base(message)
    {
    }
    public CommentNotFoundException(long id) : base($"Comment with id '{id}' not found") { }
}