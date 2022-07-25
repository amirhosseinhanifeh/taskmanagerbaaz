using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
namespace  MS.Services.TaskCatalog.Contract.Tasks.Result;
public record GetTodayTasksResult(IList<TodayTasksDto> Tasks,int TotalItemCount);
