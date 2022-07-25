using Ardalis.GuardClauses;
using MsftFramework.Abstractions.Core.Domain.Model;

namespace MS.Services.TaskCatalog.Domain.Workflows.ValueObjects
{
    public record WorkflowId : AggregateId
    {
        public WorkflowId(long value) : base(value)
        {
            Guard.Against.NegativeOrZero(value, nameof(value));
        }

        public static implicit operator long(WorkflowId id) => id.Value;

        public static implicit operator WorkflowId(long id) => new(id);
    }
}