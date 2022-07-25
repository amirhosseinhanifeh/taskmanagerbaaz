using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Projects.Exceptions.Domain;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Domain.Projects.ValueObjects
{
    public record ProjectName
    {
        public string? Value { get; private set; }

        public ProjectName? Null => null;

        public static ProjectName Create(string value)
        {
            return new ProjectName
            {
                Value = Guard.Against.NullOrEmpty(value, new ProjectDomainException("Name can't be null mor empty."))
            };
        }

        public static implicit operator ProjectName(string value) => Create(value);

        public static implicit operator string(ProjectName value) =>
            Guard.Against.Null(value.Value!, new ProjectDomainException("Name can't be null."));
    }
}