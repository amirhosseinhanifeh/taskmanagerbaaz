using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Tasks.Data;

public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Tasks.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Tasks.Task> builder)
    {
        builder.ToTable("Tasks", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Ignore(c => c.DomainEvents);

        builder.Property(x => x.Name)
            .HasColumnType(EfConstants.ColumnTypes.LongText)
            .IsRequired();

        builder.Property(h => h.Description).IsRequired();

        builder.HasOne(h => h.Category)
            .WithMany(h => h.Tasks)
            .HasForeignKey(h => h.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(x => x.ControllerUser)
            .WithMany(x => x.ControllerTasks)
            .HasForeignKey(x => x.ControllerUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(x => x.TesterUser)
            .WithMany(x => x.TesterTasks)
            .HasForeignKey(x => x.TesterUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Projects)
            .WithMany(x => x.Tasks)
            .UsingEntity(x => x.ToTable("TaskProjects"));

        builder.HasMany(x => x.Units)
.WithMany(x => x.Tasks)
.UsingEntity(x => x.ToTable("TaskUnits"));

        //builder.HasMany(x => x.Workflows)
    //.WithOne(x => x.Tasks);
        //.UsingEntity(x => x.ToTable("TaskWorkFlows"));

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Task)
            .HasForeignKey(x => x.TaskId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
