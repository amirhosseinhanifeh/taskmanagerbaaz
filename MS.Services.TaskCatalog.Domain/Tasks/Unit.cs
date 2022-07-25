using Ardalis.GuardClauses;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.Exception;
using MsftFramework.Core.Domain.Events.Internal;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class Unit : Entity<long>
    {

        public string Name { get; private set; } = default!;
        public ICollection<Task> Tasks { get; private set; } = default!;
        public static Unit Create(long id,string name)
        {
            return new Unit
            {
                Id = id,
                Name=name
            };
        }
    }
}