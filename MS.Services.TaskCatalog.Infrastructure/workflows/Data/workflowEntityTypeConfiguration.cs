using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain;
using MS.Services.TaskCatalog.Domain.Workflows;
using MS.Services.TaskCatalog.Domain.Workflows.ValueObjects;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

public class WorkflowEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Workflows.Workflow>
{
    public void Configure(EntityTypeBuilder<Domain.Workflows.Workflow> builder)
    {
        builder.ToTable("Workflows", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();


      

        builder.Ignore(c => c.DomainEvents);

        builder.Property(x => x.Name)
            .HasColumnType(EfConstants.ColumnTypes.NormalText)
            .HasConversion(name => name.Value, name => WorkflowName.Create(name!))
            .IsRequired();

        builder.HasMany(x => x.WorkflowSteps)
            .WithOne(x => x.Workflow)
            .HasForeignKey(x => x.WorkFlowId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}

public class WorkflowIntanceEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Workflows.WorkflowInstance>
{
    public void Configure(EntityTypeBuilder<Domain.Workflows.WorkflowInstance> builder)
    {
        builder.ToTable("WorkflowInstances", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();


        
        builder.Property(x => x.Name)
            .HasColumnType(EfConstants.ColumnTypes.NormalText)
            .IsRequired();


        builder.Property(x => x.Status);

        builder.HasMany(x => x.workflowSteps)
            .WithOne(x => x.WorkflowInstance)
            .HasForeignKey(x => x.WorkflowInstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Workflow)
            .WithMany(x => x.WorkflowInstances)
            .HasForeignKey(x => x.WorkflowId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}