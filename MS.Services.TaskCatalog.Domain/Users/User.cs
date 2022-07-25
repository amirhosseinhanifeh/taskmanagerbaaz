using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Domain.Users;
using MS.Services.TaskCatalog.Domain.workflows;
using MsftFramework.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class User : Entity<long>
    {
        public string Name { get; set; } = null!;
        public string? Avatar { get; set; }
        public string DeviceId { get; set; }
        public ICollection<Task> ControllerTasks { get; set; } = default!;
        public ICollection<Task> TesterTasks { get; set; } = default!;
        public ICollection<Project> Projects { get; set; } = default!;
        public ICollection<Task> Tasks { get; set; } = default!;
        public ICollection<TaskProgress> TaskProgresses { get; set; } = default!;
        public ICollection<UserSelection> SelectionUsers { get; set; } = default!;
        public ICollection<UserSelection>  UserSelections { get; set; } = default!;
        public ICollection<WorkflowRoleUser> WorkflowRoleUsers { get; set; } = default!;
        public static User Create(
            long id,
            string name,
            string avatar)
        {
            return new User
            {
                Id = id,
                Name = name,
                Avatar=avatar
            };
        }
    }
}
