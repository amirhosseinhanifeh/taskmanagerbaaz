//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
//using MsftFramework.Core.Persistence.EfCore;

//namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

//public class WorkflowAgentEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Workflows.WorkflowAgent>
//{
//    public void Configure(EntityTypeBuilder<Domain.Workflows.WorkflowAgent> builder)
//    {
//        builder.ToTable("WorkflowAgents", TaskCatalogDbContext.DefaultSchema);

//        builder.HasKey(c => c.Id);
//        builder.HasIndex(x => x.Id).IsUnique();
//        builder.Property(x => x.Name)
//            .IsRequired();
//        builder.Property(x => x.Order)
//        .IsRequired();
      
//        builder.Property(x => x.UserId)
//       .IsRequired();
//        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
//    }
//}
 