using Microsoft.AspNetCore.Mvc;
using MS.Services.TaskCatalog.Contract;
using RestEase;

namespace MS.Services.TaskCatalog.Rest.Workflows;
[BasePath(WorkflowsApiConfigs.WorkflowsPrefixUri)]
public interface IWorkflowApiClientService
{
    [Get("{id}")]
    Task<GetWorkflowByIdResponse?> Get([Path] long id,CancellationToken cancellationToken=default);
    //[Post("Create")]
    //Task<IActionResult> CreateAsync([Body] CreateWorkflowRequest command);
}