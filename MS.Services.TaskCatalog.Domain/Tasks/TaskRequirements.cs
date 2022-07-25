using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class TaskRequirements:Entity<long>
    {

        /// <summary>
        /// ترتیب
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// متن
        /// </summary>
        public string? Body { get; set; }
        /// <summary>
        /// انجام شده یا خیر
        /// </summary>
        public bool IsDone { get; set; } = false;
        public TaskId TaskId { get; set; } = default!;
        public Task Task { get; set; } = default!;

    }
}
