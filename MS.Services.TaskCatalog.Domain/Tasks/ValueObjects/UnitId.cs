using Ardalis.GuardClauses;
using MsftFramework.Abstractions.Core.Domain.Model;

namespace MS.Services.TaskCatalog.Domain.Tasks.ValueObjects
{
    public record UnitId : AggregateId
    {
        public UnitId(long value) : base(value)
        {
            Guard.Against.NegativeOrZero(value, nameof(value));
        }

        public static implicit operator long(UnitId id) => id.Value;

        public static implicit operator UnitId(long id) => new(id);
    }
}