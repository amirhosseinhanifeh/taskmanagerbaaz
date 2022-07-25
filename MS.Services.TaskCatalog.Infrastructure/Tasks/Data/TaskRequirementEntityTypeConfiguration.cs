using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Domain.Users;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Tasks.Data;

public class TaskRequirementEntityTypeConfiguration : IEntityTypeConfiguration<TaskRequirements>
{
    public void Configure(EntityTypeBuilder<TaskRequirements> builder)
    {
        builder.ToTable("TaskRequirements", TaskCatalogDbContext.DefaultSchema);
        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Body).IsRequired(true);
        builder.HasOne(x => x.Task)
            .WithMany(x => x.Requirements)
            .HasForeignKey(x => x.TaskId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}