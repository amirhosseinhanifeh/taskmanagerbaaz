using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Domain.Comments;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Domain.Workflows;
using MsftFramework.Abstractions.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure;

public interface ITaskCatalogDbContext : IDbContext,  IDisposable
{

    DbSet<Category> Categories { get; }
    DbSet<User> Users { get; }
    DbSet<Domain.Projects.Project> Projects { get; }
    DbSet<Unit> Units { get; }

    #region Task
    DbSet<Domain.Tasks.Task> Tasks { get; }
    DbSet<TaskProgress> TaskProgresses { get; }
    DbSet<TaskRequirements> TaskRequirements { get; }
    DbSet<TaskNotification> TaskNotifications { get; }

    #endregion

    #region Comment
    DbSet<Comment> Comments{ get; }
    #endregion

    DbSet<Domain.Users.UserSelection> UserSelections { get; }

    DbSet<Workflow> Workflows { get; }
    DbSet<WorkflowInstance> WorkflowInstance { get; }
    DbSet<WorkflowStepInstance> WorkflowStepInstances{ get; }
    DbSet<WorkFlowAlerts> WorkFlowAlerts { get; }
    DbSet<WorkflowRoleModel> WorkFlowRoleModels { get; }
    DbSet<Role> Roles { get; }
    DbSet<WorkflowStepAlertInstance> WorkflowStepAlertInstances { get; }

    DbSet<TEntity> Set<TEntity>()
      where TEntity : class;
    int SaveChanges();

     

}