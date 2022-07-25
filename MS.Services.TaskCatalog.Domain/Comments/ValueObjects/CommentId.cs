using Ardalis.GuardClauses;
using MsftFramework.Abstractions.Core.Domain.Model;

namespace MS.Services.TaskCatalog.Domain.Comments.ValueObjects
{
    public record CommentId : AggregateId
    {
        public CommentId(long value) : base(value)
        {
            Guard.Against.NegativeOrZero(value, nameof(value));
        }

        public static implicit operator long(CommentId id) => id.Value;

        public static implicit operator CommentId(long id) => new(id);
    }
}