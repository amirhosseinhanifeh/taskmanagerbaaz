using Ardalis.GuardClauses;
using MsftFramework.Abstractions.Core.Domain.Model;

namespace MS.Services.TaskCatalog.Domain.Tasks.ValueObjects
{
    public record TaskId : AggregateId
    {
        public TaskId(long value) : base(value)
        {
            Guard.Against.NegativeOrZero(value, nameof(value));
        }

        public static implicit operator long(TaskId id) => id.Value;

        public static implicit operator TaskId(long id) => new(id);
    }
}