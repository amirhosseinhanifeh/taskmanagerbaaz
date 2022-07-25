using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class TaskProgress:Entity<long>
    {
        public int Progress { get; set; }
        public string? Description { get; set; }
        public UserRoleType UserRole { get; set; }
        public TimeSpan EndTime { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = default!;

        public TaskId TaskId { get; set; }
        public Task Task { get; set; } = default!;
    }
}
