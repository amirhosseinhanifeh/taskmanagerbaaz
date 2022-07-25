using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

public class WorkflowRoleModelsEntityTypeConfiguration : IEntityTypeConfiguration<WorkflowRoleModel>
{
    public void Configure(EntityTypeBuilder<WorkflowRoleModel> builder)
    {
        builder.ToTable("WorkflowRoleModels", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Name)
            .IsRequired();
        builder.HasMany(x => x.Roles)
            .WithOne(x => x.WorkflowRoleModel)
            .HasForeignKey(x => x.WorkflowRoleModelId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}

