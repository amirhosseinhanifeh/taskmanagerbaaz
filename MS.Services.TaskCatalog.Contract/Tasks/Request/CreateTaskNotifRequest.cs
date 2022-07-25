using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Domain.SharedKernel;

namespace MS.Services.TaskCatalog.Contract.Tasks.Request;
public record CreateTaskNotifRequest
{
    public long TaskId { get; init; }
    public TimeSpan Time { get; set; }
    public long UserId { get; set; }


}