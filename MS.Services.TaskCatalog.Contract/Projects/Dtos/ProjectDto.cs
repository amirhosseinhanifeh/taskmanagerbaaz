using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Contract.Projects.Dtos
{
    public class ProjectDto
    {
        public long Id { get; set; }
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
    }
}
