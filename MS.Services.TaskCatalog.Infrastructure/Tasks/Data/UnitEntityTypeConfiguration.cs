using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.TaskCatalog.Domain;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
using MsftFramework.Core.Persistence.EfCore;

namespace MS.Services.TaskCatalog.Infrastructure.Tasks.Data;

public class UnitEntityTypeConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.ToTable("Units", TaskCatalogDbContext.DefaultSchema);
        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Name)
            .HasColumnType(EfConstants.ColumnTypes.NormalText)
            .IsRequired();
        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}