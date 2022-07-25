using MsftFramework.Persistence.EfCore.Postgres;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Data;
public class TaskCatalogDbContextDesignFactory : DbContextDesignFactoryBase<TaskCatalogDbContext>
{
    public TaskCatalogDbContextDesignFactory() : base("ms.services.taskCatalogConnection")
    {
    }
}