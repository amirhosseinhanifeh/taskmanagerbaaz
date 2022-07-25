using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Contract.Tasks.Dtos
{
    public class TaskDeadLineDto
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
