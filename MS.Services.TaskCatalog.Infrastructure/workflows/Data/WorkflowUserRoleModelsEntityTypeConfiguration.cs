using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

public class WorkflowUserRoleModelsEntityTypeConfiguration : IEntityTypeConfiguration<WorkflowRoleUser>
{
    public void Configure(EntityTypeBuilder<WorkflowRoleUser> builder)
    {
        builder.ToTable("WorkflowRoleUsers", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasOne(x => x.WorkflowStepInstance)
            .WithMany(x => x.WorkflowRoleUsers)
            .HasForeignKey(x => x.WorkflowStepInstanceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Role)
    .WithMany(x => x.WorkflowRoleUsers)
    .HasForeignKey(x => x.RoleId)
    .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
.WithMany(x => x.WorkflowRoleUsers)
.HasForeignKey(x => x.UserId)
.OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}

