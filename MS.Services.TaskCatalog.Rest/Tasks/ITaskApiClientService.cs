using Microsoft.AspNetCore.Mvc;
using MS.Services.TaskCatalog.Contract;
using RestEase;

namespace MS.Services.TaskCatalog.Rest.Tasks;
[BasePath(TasksApiConfigs.TasksPrefixUri)]
public interface ITaskApiClientService
{
    [Get("{id}")]
    Task<GetTaskByIdResponse?> Get([Path] long id,CancellationToken cancellationToken=default);
    //[Post("Create")]
    //Task<IActionResult> CreateAsync([Body] CreateTaskRequest command);
}