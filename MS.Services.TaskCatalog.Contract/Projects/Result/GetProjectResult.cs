using MS.Services.TaskCatalog.Contract.Projects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Contract.Projects.Result
{
    public record GetProjectResult(IList<ProjectDto> Projects);
    
}
