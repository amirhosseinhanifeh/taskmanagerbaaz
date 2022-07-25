using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Domain.Projects.ValueObjects;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Tasks.Data;

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Domain.Projects.Project> builder)
    {
        builder.ToTable("Projects", TaskCatalogDbContext.DefaultSchema);


        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnType(EfConstants.ColumnTypes.NormalText)
            .HasConversion(name => name.Value, name => ProjectName.Create(name!))
            .IsRequired();

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Projects)
            .UsingEntity(x => x.ToTable("UserProjects"));

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}