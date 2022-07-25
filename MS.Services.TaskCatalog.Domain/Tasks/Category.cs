using Ardalis.GuardClauses;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.Exception;
using MsftFramework.Core.Domain.Events.Internal;
using MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Domain;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class Category : Entity<long>
    {

        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public ICollection<Domain.Tasks.Task> Tasks { get; set; } = default!;

        public static Category Create(
    long id,
    string name,
    string description)
        {

            var category = new Category
            {
                Id = Guard.Against.Null(id, new TaskDomainException("Task id can not be null")),
                Name = name,
                Description = description,

            };

            return category;
        }

    }
}