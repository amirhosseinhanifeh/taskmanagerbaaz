using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Comments.Exceptions.Domain;
using MS.Services.TaskCatalog.Domain.Comments.ValueObjects;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.Exception;
using MsftFramework.Core.Domain.Events.Internal;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;

namespace MS.Services.TaskCatalog.Domain.Comments
{
    public class Comment : Entity<long>
    {
        public TaskId TaskId { get; private set; } = default!;
        public Domain.Tasks.Task Task { get; set; } = null!;
        public string? Body { get; private set; }

        public long? CommentId { get; private set; }
        public Comment CommentModel { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = null!;
        public static Comment Create(
           long id,
           string? body,
           TaskId taskId,
           long? commentId)
        {
            var comment = new Comment
            {
                Id = Guard.Against.Null(id, new CommentDomainException("Comment id can not be null")),
                Body = Guard.Against.Null(body, new CommentDomainException("Comment body can not be null")),
                TaskId = Guard.Against.Null(taskId, new CommentDomainException("Comment body can not be null")),
                CommentId=commentId,
            };

            return comment;
        }
    }
}