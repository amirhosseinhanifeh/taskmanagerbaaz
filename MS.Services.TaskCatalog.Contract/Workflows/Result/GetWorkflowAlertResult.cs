using MS.Services.TaskCatalog.Contract.workflows.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Contract.workflows.Result;
public record GetWorkflowAlertsResult(IList<WorkflowAlertDto> Alerts);

