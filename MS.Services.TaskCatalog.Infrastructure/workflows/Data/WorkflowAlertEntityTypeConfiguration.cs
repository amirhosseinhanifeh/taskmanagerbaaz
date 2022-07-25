using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

public class WorkflowAlertEntityTypeConfiguration : IEntityTypeConfiguration<WorkFlowAlerts>
{
    public void Configure(EntityTypeBuilder<WorkFlowAlerts> builder)
    {
        builder.ToTable("WorkFlowAlerts", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Body)
            .IsRequired();
        builder.HasMany(x => x.WorkflowStepAlerts)
            .WithOne(x => x.WorkFlowAlert)
            .HasForeignKey(x => x.WorkflowAlertId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}

public class WorkflowStepAlertEntityTypeConfiguration : IEntityTypeConfiguration<WorkflowStepAlerts>
{
    public void Configure(EntityTypeBuilder<WorkflowStepAlerts> builder)
    {
        builder.ToTable("WorkflowStepAlerts", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();



        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
public class WorkflowStepAlertInstanceEntityTypeConfiguration : IEntityTypeConfiguration<WorkflowStepAlertInstance>
{
    public void Configure(EntityTypeBuilder<WorkflowStepAlertInstance> builder)
    {
        builder.ToTable("WorkflowStepAlertInstance", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();



        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
