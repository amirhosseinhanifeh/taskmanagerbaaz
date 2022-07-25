using Ardalis.GuardClauses;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.Exception;
using MsftFramework.Core.Domain.Events.Internal;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class UnitTask : Entity<long>
    {
        public Unit  Unit { get; set; }

        public UnitId UnitId { get; set; }

        public TaskId TaskId { get; set; }
        public Task  Task { get; set; }
    }
}