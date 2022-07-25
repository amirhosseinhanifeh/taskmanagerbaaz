using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class TaskImage : Entity<long>
    {

        public TaskId TaskId { get; set; }
        public Task Task { get; set; }

        public long ImageId { get; set; }
    }
}
