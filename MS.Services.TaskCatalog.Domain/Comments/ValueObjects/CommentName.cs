using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Comments.Exceptions.Domain;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Domain.Comments.ValueObjects
{
    public record CommentName
    {
        public string? Value { get; private set; }

        public CommentName? Null => null;

        public static CommentName Create(string value)
        {
            return new CommentName
            {
                Value = Guard.Against.NullOrEmpty(value, new CommentDomainException("Name can't be null mor empty."))
            };
        }

        public static implicit operator CommentName(string value) => Create(value);

        public static implicit operator string(CommentName value) =>
            Guard.Against.Null(value.Value!, new CommentDomainException("Name can't be null."));
    }
}