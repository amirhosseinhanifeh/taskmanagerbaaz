using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Tasks.Data;

public class TaskNotifTimeEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Tasks.TaskNotification>
{
    public void Configure(EntityTypeBuilder<Domain.Tasks.TaskNotification> builder)
    {
        builder.ToTable("TaskNotifications", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.HasOne(x => x.Task)
            .WithMany(x => x.TaskNotifications)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
