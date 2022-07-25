using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

public class WorkflowStepInstanceEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Workflows.WorkflowStepInstance>
{
    public void Configure(EntityTypeBuilder<Domain.Workflows.WorkflowStepInstance> builder)
    {
        builder.ToTable("WorkflowStepInstance", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Name)
            .IsRequired();
        builder.Property(x => x.WorkflowInstanceId)
           .IsRequired();
        builder.Property(x => x.Order)
       .IsRequired();
        builder.Property(x => x.LastVisit);

        builder.Property(x => x.DeadLine)
         .IsRequired();
        builder.Property(x => x.WorkflowInstanceId)
        .IsRequired();
        builder.Property(x => x.Status)
      .IsRequired();
        builder.Property(x => x.TaskId)
               .HasConversion(x => x.Value, id => id)
               .ValueGeneratedNever();

        builder.HasMany(x => x.WorkflowRoleUsers)
            .WithOne(x => x.WorkflowStepInstance)
            .HasForeignKey(x => x.WorkflowStepInstanceId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
 