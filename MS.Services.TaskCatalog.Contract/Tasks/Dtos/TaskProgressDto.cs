using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Contract.Tasks.Dtos
{
    public class TaskProgressDto
    {
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public int Progress { get; set; }
    }
}
