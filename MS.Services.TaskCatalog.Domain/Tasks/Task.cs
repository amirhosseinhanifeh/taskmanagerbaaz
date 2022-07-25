using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Comments;
using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.Features.CreatingTask.Events.Domain;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Domain.Workflows;
using MsftFramework.Core.Domain.Events.Internal;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.Exception;
using Newtonsoft.Json;

namespace MS.Services.TaskCatalog.Domain.Tasks
{
    public class Task : Aggregate<TaskId>
    {
        /// <summary>
        /// عنوان تسک
        /// </summary>
        public string Name { get; private set; } = default!;

        /// <summary>
        /// متن تسک
        /// </summary>
        public string Description { get; private set; } = default!;
        /// <summary>
        /// زمان شروع تسک
        /// </summary>
        public DateTime StartDateTime { get; private set; }
        /// <summary>
        /// زمان پایان تسک
        /// </summary>
        public DateTime EndDateTime { get; private set; }
        /// <summary>
        /// دسته بندی
        /// </summary>
        public long? CategoryId { get; private set; }
        public Category Category { get; private set; } = default!;

        /// <summary>
        /// تسک امروز
        /// </summary>
        public bool IsTodayTask { get; private set; } = false;

        public int ReturnCount { get; private set; } = 0;

        /// <summary>
        /// اهمیت
        /// </summary>
        public priorityType Priority { get; private set; }
        /// <summary>
        /// شناسه صدا
        /// </summary>
        public long? VoiceId { get; private set; }
        /// <summary>
        /// اهمیت
        /// </summary>
        public ImportanceType ImportanceType { get; private set; }

        /// <summary>
        /// ترتیب
        /// </summary>
        public int Order { get; private set; } = 0;
        /// <summary>
        /// اولویت مدیر
        /// </summary>
        public int ManagerOrder { get; private set; } = 0;


        /// <summary>
        /// قفل کردن از سمت مدیریت
        /// </summary>
        public bool DragLocked { get; private set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public Domain.SharedKernel.TaskStatus? Status { get; private set; }

        /// <summary>
        /// پایان کنترلر
        /// </summary>
        public string? ControllerEndTime { get; private set; }


        /// <summary>
        /// سیستم یادآور
        /// </summary>
        public int? AlartReminder { get; private set; }
        /// <summary>
        /// کاربر کنترل کننده
        /// </summary>
        public long? ControllerUserId { get; private set; }
        public User ControllerUser { get; private set; } = null!;
        /// <summary>
        /// کاربر تستر
        /// </summary>
        public long? TesterUserId { get; private set; }
        public User TesterUser { get; private set; } = null!;
        public TaskDeadLine TaskDeadLine { get; private set; } = default!;
        public ICollection<Unit> Units { get; private set; } = default!;
        public ICollection<User> Users { get; private set; } = null!;
        public ICollection<TaskImage> Images { get; set; } = null!;
        public ICollection<Comment> Comments { get; private set; } = null!;
        public ICollection<Project> Projects { get; private set; } = default!;
        public ICollection<TaskProgress> TaskProgresses { get; private set; } = default!;
        //public ICollection<Workflow> Workflows { get; private set; } = default!;
        public ICollection<TaskRequirements> Requirements { get; private set; } = default!;
        public ICollection<TaskNotification> TaskNotifications { get; private set; } = default!;

        public static Task Create(
            TaskId id,
            string name,
            DateTime? startDateTime,
            DateTime endDateTime,
            priorityType priority,
            ImportanceType importanceType,
            string description,
            long? voiceId,
            int creatorUserId,
            long? controllerUserId,
            long? testerUserId,
            Domain.SharedKernel.TaskStatus? status)
        {
            //DomainEventsHandler.RaiseDomainEvent(new CreatingTaskEvent(id, name, description));

            var Task = new Task
            {
                Id = Guard.Against.Null(id, new TaskDomainException("Task id can not be null")),
                Name = Guard.Against.CheckLength(name, 250).Null(name, new TaskDomainException("Task name can not be null")),
                CategoryId = 1,
                Description = Guard.Against.NullOrEmpty(description, new TaskDomainException("Description is can not be null")),
                EndDateTime = endDateTime,
                StartDateTime = startDateTime.GetValueOrDefault(),
                Priority = priority,
                VoiceId = voiceId,
                ControllerUserId = controllerUserId,
                TesterUserId = testerUserId,
                ImportanceType = importanceType,
                CreatedBy = creatorUserId,
                Status = status,

            };

            Task.ChangeName(name);

            Task.AddDomainEvent(new TaskCreatedEvent(Task));

            return Task;
        }

        public void Update(
            TaskId id,
            string name,
            DateTime? startDateTime,
            DateTime endDateTime,
            priorityType priority,
            ImportanceType importanceType,
            string description,
            long? voiceId,
            long creatorUserId,
            long? controllerUserId,
            long? testerUserId
            )
        {
            //DomainEventsHandler.RaiseDomainEvent(new CreatingTaskEvent(id, name, description));
            Id = Guard.Against.Null(id, new TaskDomainException("Task id can not be null"));
            Name = Guard.Against.CheckLength(name, 250).Null(name, new TaskDomainException("Task name can not be null"));
            CategoryId = 1;
            Description = Guard.Against.NullOrEmpty(description, new TaskDomainException("Description is can not be null"));
            EndDateTime = endDateTime;
            StartDateTime = startDateTime.GetValueOrDefault();
            Priority = priority;
            VoiceId = voiceId;
            if (controllerUserId != null)
                ControllerUserId = Guard.Against.NegativeOrZero((int)controllerUserId, "کنترلر نمیتواند صفر باشد");
            if (testerUserId != null)
                TesterUserId = Guard.Against.NegativeOrZero((int)testerUserId, "تستر نمیتواند صفر باشد"); ;
            ImportanceType = importanceType;

        }
        /// <summary>
        /// Sets Task item name.
        /// </summary>
        /// <param name="name">The name to be changed.</param>
        public void ChangeName(TaskName name)
        {
            Guard.Against.Null(name, new TaskDomainException("Task name cannot be null."));

            Name = name;
        }
        public void AddProject(Project[] projectIds)
        {
            if (projectIds != null)
            {
                Projects = new List<Project>();
                foreach (var item in projectIds)
                {
                    Projects.Add(item);

                }
            }
        }
        public void AddRequirements(string[]? requirements)
        {
            if (requirements != null)
            {
                Requirements = new List<TaskRequirements>();
                foreach (var item in requirements)
                {
                    Requirements.Add(new TaskRequirements
                    {
                        Body = item,
                        IsDone = false,
                        Order = Array.IndexOf(requirements, item)
                    });
                }
            }
        }
        public void AddUnit(Unit[] unitIds)
        {
            if (unitIds != null)
            {
                Units = new List<Unit>();
                foreach (var item in unitIds)
                {
                    Units.Add(item);

                }
            }
        }
        public void AssignTaskToUsers(User[] userIds)
        {
            if (userIds != null)
            {
                Users = new List<User>();
                foreach (var item in userIds)
                {
                    Users.Add(item);
                }
            }
        }
        public void AddImages(TaskImage[] images)
        {
            if (images != null)
            {
                Images = new List<TaskImage>();
                foreach (var item in images)
                {
                    Images.Add(item);
                }
            }
        }
        /// <summary>
        /// add deadline
        /// </summary>
        /// <param name="time"></param>
        /// <param name="date"></param>
        public void AddDeadLine(TimeSpan time, DateTime date)
        {
            TaskDeadLine = new TaskDeadLine
            {
                Time = time,
                Date = date
            };
        }
        /// <summary>
        /// update task status
        /// </summary>
        /// <param name="status"></param>
        public void ChangeStatus(Domain.SharedKernel.TaskStatus? status)
        {
            Status = status;
        }

        /// <summary>
        /// update task order
        /// </summary>
        /// <param name="status"></param>
        public void SetOrder(int order)
        {
            Order = order;
        }

        /// <summary>
        /// set Today Task
        /// </summary>
        /// <param name="isTodayTask"></param>
        public void SetTodayTask(bool isTodayTask)
        {
            IsTodayTask = isTodayTask;
        }
        /// <summary>
        /// change date
        /// </summary>
        /// <param name="date"></param>
        public void ChangeDate(DateTime date)
        {
            StartDateTime = date;
        }
        /// <summary>
        /// set return count
        /// </summary>
        /// <param name="returnCount"></param>
        public void SetReturnCount(int returnCount)
        {
            ReturnCount = returnCount;
        }
        public void UpdateTaskToday(int? alertReminder, TimeSpan? userStartTime, TimeSpan? userEndTime, string? cEndTime, int? cProgress, int? uProgress)
        {
            AlartReminder = alertReminder;
            if (userStartTime != null)
                StartDateTime = StartDateTime.Date + userStartTime.GetValueOrDefault();
            if (userEndTime != null)
                EndDateTime = EndDateTime.Date + userEndTime.GetValueOrDefault();

            ControllerEndTime = cEndTime;
            if (Users != null && Users.Any())
            {
                TaskProgresses.Add(new TaskProgress
                {
                    UserRole = UserRoleType.User,
                    Progress = uProgress.GetValueOrDefault(),
                    Description = "",
                    EndTime = TimeSpan.FromHours(1),
                    UserId = Users.FirstOrDefault().Id
                });
            }
            if (ControllerUserId != null)
            {
                TaskProgresses.Add(new TaskProgress
                {
                    UserRole = UserRoleType.Controller,
                    Progress = cProgress.GetValueOrDefault(),
                    Description = "",
                    EndTime = TimeSpan.FromHours(1),
                    UserId = ControllerUserId.Value
                });

            }
            //if ()
            //    TaskProgresses.Add(new TaskProgress {
            //        UserRole = UserRoleType.Controller,
            //        UserId = ControllerUserId,

            //    });            
        }
    }
}