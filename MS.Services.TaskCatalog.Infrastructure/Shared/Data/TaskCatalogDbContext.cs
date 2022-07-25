using Microsoft.EntityFrameworkCore;
using MS.Services.TaskCatalog.Domain.Comments;
using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Users;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Domain.Workflows;
using MsftFramework.Abstractions.Core.Domain.Events.Internal;
using MsftFramework.Core.Persistence.EfCore;
using System.Reflection;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Data;
public class TaskCatalogDbContext : EfDbContextBase, ITaskCatalogDbContext
{
    public const string DefaultSchema = "taskCatalog";

    public TaskCatalogDbContext(DbContextOptions options) : base(options)
    {
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension(EfConstants.UuidGenerator);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Domain.Tasks.Task> Tasks => Set<Domain.Tasks.Task>();
    public DbSet<TaskProgress> TaskProgresses => Set<TaskProgress>();
    public DbSet<TaskRequirements> TaskRequirements => Set<TaskRequirements>();
    public DbSet<TaskNotification> TaskNotifications => Set<TaskNotification>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Unit> Units => Set<Unit>();

    public DbSet<Domain.Workflows.Workflow> Workflows => Set<Workflow>();
    public DbSet<Domain.Workflows.WorkflowStep> WorkflowSteps => Set<WorkflowStep>();

    public DbSet<UserSelection> UserSelections => Set<UserSelection>();

    public DbSet<Comment> Comments => Set<Comment>();

    public DbSet<WorkflowInstance> WorkflowInstance => Set<WorkflowInstance>();
    public DbSet<WorkflowStepInstance> WorkflowStepInstances => Set<WorkflowStepInstance>();
    public DbSet<WorkFlowAlerts> WorkFlowAlerts => Set<WorkFlowAlerts>();
    public DbSet<WorkflowRoleModel> WorkFlowRoleModels => Set<WorkflowRoleModel>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<WorkflowStepAlertInstance> WorkflowStepAlertInstances => Set<WorkflowStepAlertInstance>();

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
    public override void Dispose()
    {

        base.Dispose();
    }
}