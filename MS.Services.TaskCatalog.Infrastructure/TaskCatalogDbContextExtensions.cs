using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Domain.Users;
using MS.Services.TaskCatalog.Domain.Workflows;
using MsftFramework.Abstractions.Core.Domain.Model;
using MsftFramework.Core.Domain.Model;

namespace MS.Services.TaskCatalog.Infrastructure;

public static class TaskCatalogDbContextExtensions
{



    #region Category Extention Methods
    public static async ValueTask<List<Category>> GetCategoriesAsync(this ITaskCatalogDbContext context, CancellationToken cancellationToken = default)
    {
        return await context.Categories.ToListAsync(cancellationToken);
    }

    #endregion

    #region Task Extention Methods
    public static async ValueTask<List<TaskProgress>> GetProgressesByTaskIdAsync(this ITaskCatalogDbContext context, long taskId, CancellationToken cancellationToken = default)
    {
        return await context.TaskProgresses.Include(x => x.User).Where(x => x.TaskId == taskId).ToListAsync(cancellationToken);
    }
    public static IQueryable<Domain.Tasks.Task> GetTodayTasksAsync(this ITaskCatalogDbContext context,
    long userId,
    TodayOrderPriority? orderPriority = null)
    {
        var res = context.Tasks
            .Include(x => x.TaskProgresses)
            .Where(x => x.IsTodayTask == true)
            .AsQueryable();

        if (orderPriority != null)
        {
            res = orderPriority switch
            {
                TodayOrderPriority.Admin => res = res.OrderBy(x => x.ManagerOrder),
                TodayOrderPriority.User => res = res.OrderBy(x => x.Order),
                _ => throw new NotImplementedException()
            };
        }
        else
        {
            res = res.OrderBy(x => x.Order);
        }

        return res.AsQueryable();
    }
    public static async ValueTask<Domain.Tasks.Task?> FindTaskByIdAsync(this ITaskCatalogDbContext context, long id, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .Include(x=>x.Users)
            .Include(x=>x.Units)
            .Include(x=>x.Projects)
            .Include(x=>x.Requirements)
            .Include(x => x.Images)
            .Include(x => x.TaskDeadLine)
            .Include(x => x.Comments)
            .ThenInclude(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public static IQueryable<Domain.Tasks.Task> GetTasks(this ITaskCatalogDbContext context,
        string? name,
        long[]? projectIds,
        long[]? unitIds,
        long[]? userIds,
        long? controllerId,
        long? testerId,
        long? creatoruserId,
        bool? istodayTask,
        DateTime? startDate,
        DateTime? endDate,
        priorityType? priorityType,
        TaskSort? sort = TaskSort.New,
TodayOrderPriority? orderPriority = null)
    {
        var res = context.Tasks
            .Include(x => x.TaskProgresses)
            .AsQueryable();

        if(istodayTask !=null)
        {
            res = res.Where(x => x.IsTodayTask == istodayTask);
        }
        if (!string.IsNullOrEmpty(name))
        {
            var keywords = name.Split(" ");
            foreach (var item in keywords)
            {
                res = res.Where(x => x.Name.Contains(item));
            }
        }

        if (projectIds != null)
            res = res.Include(x => x.Projects).Where(x => x.Projects.Any(h => projectIds.Contains(h.Id)));

        if (unitIds != null)
            res = res.Include(x => x.Units).Where(res => res.Units.Any(h => unitIds.Contains(h.Id)));

        if (startDate != null)
            res = res.Where(x => x.StartDateTime.Date <= startDate.Value.Date);

        if (endDate != null)
            res = res.Where(x => x.EndDateTime >= endDate);

        if (priorityType != null)
            res = res.Where(x => x.Priority == priorityType);

        if (userIds != null)
            res = res.Include(x => x.Users).Where(x => x.Users.Any(h => userIds.Contains(h.Id)));

        if (controllerId != null)
            res = res.Where(x => x.ControllerUserId == controllerId);

        if (testerId != null)
            res = res.Where(x => x.TesterUserId == testerId);

        if (creatoruserId != null)
            res = res.Where(x => x.CreatedBy == creatoruserId);

        if (sort != null)
        {
            res = sort switch
            {
                TaskSort.New => res = res.OrderByDescending(x => x.Created),
                TaskSort.Old => res = res.OrderBy(x => x.Created),
                TaskSort.Delay => res = res.OrderByDescending(x => (DateTime.Now - x.EndDateTime).Days),
                TaskSort.Priority => res = res.OrderBy(x => (int)x.Priority),
                TaskSort.Importance => res = res.OrderBy(x => (int)x.ImportanceType),
                TaskSort.ReturnCount => res = res.OrderByDescending(x => (int)x.ReturnCount),
                _ => throw new NotImplementedException()
            };
        }
        if (orderPriority != null)
        {
            res = orderPriority switch
            {
                TodayOrderPriority.Admin => res = res.OrderBy(x => x.ManagerOrder),
                TodayOrderPriority.User => res = res.OrderBy(x => x.Order),
                _ => throw new NotImplementedException()
            };
        }
        return res.AsQueryable();
    }
    #endregion

    #region User Extention Methods
    public static async ValueTask<List<User>> GetUsersByIds(this ITaskCatalogDbContext context, long[] userIds, CancellationToken cancellationToken)
    {
        if (userIds != null)
            return await context.Users.Where(x => userIds.Contains(x.Id)).ToListAsync(cancellationToken);
        return null;
    }
    public static async ValueTask<List<UserSelection>> GetUsersAsync(this ITaskCatalogDbContext context, long? userId, CancellationToken cancellationToken = default)
    {
        var res = context.UserSelections.Include(x => x.User).Include(x => x.SelectUser).AsQueryable();
        if (userId != null)
            res = res.Where(x => x.UserId == userId);

        return await res.ToListAsync(cancellationToken);
    }


    public static async ValueTask<List<Workflow>> GetAllWorkflowAsync(
    this ITaskCatalogDbContext context,
    CancellationToken cancellationToken = default)
    {
        return await context.Workflows.Include(x => x.WorkflowSteps).ToListAsync(cancellationToken);
    }

    public static async ValueTask<Workflow?> GetWorkflowByIdAsync(
    this ITaskCatalogDbContext context,
    long workFlowId,
   CancellationToken cancellationToken = default)
    {
        return await context.Workflows.Include(x=>x.WorkflowSteps).ThenInclude(x=>x.WorkflowRoleModel).ThenInclude(x=>x.Roles).Where(x => x.Id == workFlowId).FirstOrDefaultAsync(cancellationToken);
    }


    #endregion

    #region Project Extention Methods
    public static async ValueTask<List<Project>> GetProjectsByIds(this ITaskCatalogDbContext context, long[] projectIds, CancellationToken cancellationToken)
    {
        if (projectIds != null)
            return await context.Projects.Where(x => projectIds.Contains(x.Id)).ToListAsync(cancellationToken);
        return null;
    }
    public static async ValueTask<List<Project>> GetProjectsAsync(this ITaskCatalogDbContext context, long? userId, CancellationToken cancellationToken = default)
    {
        var res = context.Projects.AsQueryable();

        if (userId != null)
            res = res.Where(x => x.Users.Any(h => h.Id == userId));

        return await res.ToListAsync(cancellationToken);
    }

    public static async ValueTask<Project?> FindProjectByIdAsync(this ITaskCatalogDbContext context, long id, CancellationToken cancellationToken = default)
    {
        return await context.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    #endregion

    #region Workflow Extention Methods
    public static async Task<bool> ExistsWorkflowAsync(
       this ITaskCatalogDbContext context,
       long id,
       CancellationToken cancellationToken = default)
    {
        return await context.Workflows.AnyAsync(x => x.Id == id, cancellationToken);
    }
    public static async ValueTask<Workflow?> FindWorkflowByIdAsync(
      this ITaskCatalogDbContext context,
      long id,
      CancellationToken cancellationToken = default)
    {
        return await context.Workflows.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public static async ValueTask<List<Workflow>> FindWorkflowsAsync(
    this ITaskCatalogDbContext context,
    CancellationToken cancellationToken = default)
    {
        return await context.Workflows.ToListAsync(cancellationToken);
    }

    //public static async ValueTask<List<WorkflowSteps>> GetAllWorkflowStepsAsync(
    //this ITaskCatalogDbContext context,
    //CancellationToken cancellationToken = default)
    //{

    //    return await context.WorkflowSteps.ToListAsync(cancellationToken);
    //}

    //public static async ValueTask<WorkflowSteps?> GetWorkflowStepsByIdAsync(
    //this ITaskCatalogDbContext context,
    //long WorkflowStepsId,
    //CancellationToken cancellationToken = default)
    //{
    //    return await context.WorkflowSteps.Where(x => x.Id == WorkflowStepsId).FirstOrDefaultAsync(cancellationToken);
    //}

    //public static async ValueTask<List<UserController>> GetAllUserControllersAsync(
    //this ITaskCatalogDbContext context,
    //CancellationToken cancellationToken = default)
    //{
    //    return await context.UserControllers.ToListAsync(cancellationToken);
    //}

    //public static async ValueTask<UserController?> GetUserControllerByIdAsync(
    //this ITaskCatalogDbContext context,
    //long userControllerId,
    //CancellationToken cancellationToken = default)
    //{
    //    return await context.UserControllers.Where(x => x.Id == userControllerId).FirstOrDefaultAsync(cancellationToken);
    //}
    #endregion
}
