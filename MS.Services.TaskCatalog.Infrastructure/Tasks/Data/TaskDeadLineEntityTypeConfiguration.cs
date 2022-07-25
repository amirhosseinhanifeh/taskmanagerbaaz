using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain;
using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Tasks.Data;

public class TaskDeadLineEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Tasks.TaskDeadLine>
{
    public void Configure(EntityTypeBuilder<Domain.Tasks.TaskDeadLine> builder)
    {
        builder.ToTable("TaskDeadLines", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.HasOne(x => x.Task)
                    .WithOne(x => x.TaskDeadLine)
                    .HasForeignKey<TaskDeadLine>(x=>x.Id);


        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}