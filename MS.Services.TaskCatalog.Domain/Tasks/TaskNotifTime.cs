using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Core.Domain.Model;


namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class TaskNotification : Entity<long>
    {
        public string? Title { get; set; }
        public TaskId TaskId { get; set; }
        public Task Task { get; set; } = default!;


        public long? UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
