using AutoMapper;
using MS.Services.TaskCatalog.Contract.Comments.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Domain.Comments;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.DateTimeExtensions;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.EnumBuilderExtensions;
using System.Globalization;

namespace MS.Services.TaskCatalog.Application.Tasks;
public class TaskMappers : Profile
{
    public TaskMappers()
    {
        CreateMap<Domain.Tasks.Task, TaskDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
            .ForMember(x => x.EndDateTime, opt => opt.MapFrom(x => x.EndDateTime))
            .ForMember(x => x.StartDateTime, opt => opt.MapFrom(x => x.StartDateTime))
            .ForMember(x => x.Priority, opt => opt.MapFrom(x => (int)x.Priority))
            .ForMember(x => x.ImportanceType, opt => opt.MapFrom(x => (short)x.ImportanceType))
            .ForMember(x => x.UnitIds, opt => opt.MapFrom(x => x.Units.Select(x => x.Id).ToArray()))
            .ForMember(x => x.UserIds, opt => opt.MapFrom(x => x.Users.Select(x => x.Id).ToArray()))
            .ForMember(x => x.ProjectIds, opt => opt.MapFrom(x => x.Projects.Select(x => x.Id).ToArray()))
            .ForMember(x => x.VoiceId, opt => opt.MapFrom(x => x.VoiceId))
            .ForMember(x => x.ControllerUserId, opt => opt.MapFrom(x => x.ControllerUserId))
            .ForMember(x => x.TesterUserId, opt => opt.MapFrom(x => x.TesterUserId))
            .ForMember(x => x.CreatorUserId, opt => opt.MapFrom(x => x.CreatedBy))
            .ForMember(x => x.DeadLine, opt => opt.MapFrom(x => x.TaskDeadLine))
            .ForMember(x => x.ImageIds, opt => opt.MapFrom(x => x.Images.Select(h => h.ImageId).ToArray()))
            .ForMember(x => x.Requirements, opt => opt.MapFrom(x => x.Requirements.Select(h => h.Body).ToArray()))
            .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.Where(x => x.CommentId == null)));




        CreateMap<TaskDeadLine, TaskDeadLineDto>()
            .ForMember(x => x.Time, opt => opt.MapFrom(x => x.Time))
            .ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date));

        CreateMap<TaskProgress, TaskProgressDto>()
            .ForMember(x => x.Progress, opt => opt.MapFrom(x => x.Progress))
            .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.User.Name))
            .ForMember(x => x.Role, opt => opt.MapFrom(x => x.UserRole.ToDisplay()));


        CreateMap<Domain.Tasks.Task, TasksDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.CreateDate, opt => opt.MapFrom(x => x.Created.ConvertToDayAgo()))
            .ForMember(x => x.ReturnCount, opt => opt.MapFrom(x => x.ReturnCount))
            .ForMember(x => x.DragLocked, opt => opt.MapFrom(x => x.DragLocked))
            .ForMember(x => x.UserProgress, opt => opt.MapFrom(x => x.TaskProgresses != null ? x.TaskProgresses!.FirstOrDefault(h => h.UserRole == Domain.SharedKernel.UserRoleType.User)!.Progress : 0))
            .ForMember(x => x.ControllerProgress, opt => opt.MapFrom(x => x.TaskProgresses != null ? x.TaskProgresses!.FirstOrDefault(h => h.UserRole == Domain.SharedKernel.UserRoleType.Controller)!.Progress : 0));
        //.ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));

        CreateMap<Domain.Tasks.Task, TodayTasksDto>()
    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
    .ForMember(x => x.CreateDate, opt => opt.MapFrom(x => x.Created.ConvertToDayAgo()))
    .ForMember(x => x.ReturnCount, opt => opt.MapFrom(x => x.ReturnCount))
    .ForMember(x => x.DragLocked, opt => opt.MapFrom(x => x.DragLocked))
    .ForMember(x => x.UserProgress, opt => opt.MapFrom(x => x.TaskProgresses != null ? x.TaskProgresses!.LastOrDefault(h => h.UserRole == Domain.SharedKernel.UserRoleType.User)!.Progress : 0))
    .ForMember(x => x.ControllerProgress, opt => opt.MapFrom(x => x.TaskProgresses != null ? x.TaskProgresses!.LastOrDefault(h => h.UserRole == Domain.SharedKernel.UserRoleType.Controller)!.Progress : 0))
    .ForMember(x => x.ControllerEndTime, opt => opt.MapFrom(x => x.ControllerEndTime))
    .ForMember(x => x.UserEndTime, opt => opt.MapFrom(x => x.EndDateTime.ToString("HH:MM")))
    .ForMember(x => x.CreatorStart, opt => opt.MapFrom(x => x.StartDateTime.ToString("HH:MM")))
    .ForMember(x => x.AlartReminder, opt => opt.MapFrom(x => x.AlartReminder))
    .ForMember(x => x.HasController, opt => opt.MapFrom(x => x.ControllerUserId !=null ))
    .ForMember(x => x.HasUser, opt => opt.MapFrom(x => x.Users.Any()))
    ;

        CreateMap<Comment, CommentDto>();

        CreateMap<CreateTaskCommand, Domain.Tasks.Task>();



        CreateMap<CreateTaskRequest, CreateTaskCommand>()
            .ConstructUsing(req => new CreateTaskCommand(
                req.Name, req.StartDateTime, req.EndDateTime, req.ProjectIds, req.UnitIds
                , req.Priority, req.ImportanceType, req.Description, req.VoiceId, req.UserIds, req.ControllerUserId, req.TesterUserId,
                req.DeadLine, req.Requirements, req.ImageIds));

        CreateMap<UpdateTaskRequest, UpdateTaskCommand>()
            .ConstructUsing(req => new UpdateTaskCommand(
                req.Name, req.StartDateTime, req.EndDateTime, req.ProjectIds, req.UnitIds, req.Priority, req.ImportanceType, req.IsTodayTask, req.Description, req.VoiceId, req.UserIds, req.ControllerUserId, req.TesterUserId, req.DeadLine, req.Requirements,req.ImageIds));

        CreateMap<UpdateTaskOrderRequest, UpdateTaskOrderCommand>()
.ConstructUsing(req => new UpdateTaskOrderCommand(
req.Ids));
        CreateMap<UpdateTaskToTodayRequest, UpdateTaskToTodayCommand>()
.ConstructUsing(req => new UpdateTaskToTodayCommand(
    req.Id, req.IsToday));
        CreateMap<UpdateTaskTodayRequest, UpdateTaskTodayCommand>()
.ConstructUsing(req => new UpdateTaskTodayCommand(
req.Id, req.UStartTime, req.UEndTime, req.CEndTime, req.AlertRemainder, req.UserProgress, req.CProgress));
        CreateMap<CreateTaskNotifRequest, AddTaskNotifCommand>()
.ConstructUsing(req => new AddTaskNotifCommand(
        req.TaskId, req.Time, req.UserId));
    }
}