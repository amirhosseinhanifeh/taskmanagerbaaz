using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

public class RolesEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", TaskCatalogDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Name)
            .IsRequired();


        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}

