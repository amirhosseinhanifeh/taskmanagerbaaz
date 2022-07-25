using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Contract.workflows.Dtos
{
    public record WorkflowAlertDto
    {
        public long Id { get; set; }

        public string Body { get; set; }
    }
}
