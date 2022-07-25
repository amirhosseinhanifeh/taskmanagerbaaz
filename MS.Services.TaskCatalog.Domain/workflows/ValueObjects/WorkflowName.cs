using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Domain;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Domain.Workflows.ValueObjects
{
    public record WorkflowName
    {
        public string? Value { get; private set; }

        public WorkflowName? Null => null;

        public static WorkflowName Create(string value)
        {
            return new WorkflowName
            {
                Value = Guard.Against.NullOrEmpty(value, new WorkflowDomainException("Name can't be null mor empty."))
            };
        }

        public static implicit operator WorkflowName(string value) => Create(value);

        public static implicit operator string(WorkflowName value) =>
            Guard.Against.Null(value.Value!, new WorkflowDomainException("Name can't be null."));
    }
}