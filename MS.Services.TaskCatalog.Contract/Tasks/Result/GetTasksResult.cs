using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
namespace  MS.Services.TaskCatalog.Contract.Tasks.Result;
public record GetTasksResult(IList<TasksDto> Tasks,IList<TasksDto> TodayTasks,IList<TasksDto> UnDoneTasks,IList<TasksDto> DoneTasks,IList<TasksDto> UnCompleteTasks,int TotalItemCount);
