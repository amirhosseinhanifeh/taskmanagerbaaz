using Ardalis.GuardClauses;
using MsftFramework.Abstractions.Core.Domain.Model;

namespace MS.Services.TaskCatalog.Domain.Projects.ValueObjects
{
    public record ProjectId : AggregateId
    {
        public ProjectId(long value) : base(value)
        {
            Guard.Against.NegativeOrZero(value, nameof(value));
        }

        public static implicit operator long(ProjectId id) => id.Value;

        public static implicit operator ProjectId(long id) => new(id);
    }
}