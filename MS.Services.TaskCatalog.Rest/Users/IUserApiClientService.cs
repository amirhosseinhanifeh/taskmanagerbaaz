using Microsoft.AspNetCore.Mvc;
using MS.Services.TaskCatalog.Contract;
using RestEase;

namespace MS.Services.TaskCatalog.Rest.Tasks;
[BasePath(TasksApiConfigs.TasksPrefixUri)]
public interface IUserApiClientService
{
    [Get("{id}")]
    Task<GetUserByIdResponse?> Get([Path] Guid id,CancellationToken cancellationToken=default);
    //[Post("Create")]
    //Task<IActionResult> CreateAsync([Body] CreateTaskRequest command);
}