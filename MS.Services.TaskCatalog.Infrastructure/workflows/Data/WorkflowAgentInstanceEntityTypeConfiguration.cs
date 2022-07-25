//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using MS.Services.TaskCatalog.Infrastructure.Shared.Data;
//using MsftFramework.Core.Persistence.EfCore;

//namespace MS.Services.TaskCatalog.Infrastructure.Workflows.Data;

//public class WorkflowAgentInstanceEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Workflows.WorkflowAgentInstance>
//{
//    public void Configure(EntityTypeBuilder<Domain.Workflows.WorkflowAgentInstance> builder)
//    {
//        builder.ToTable("WorkflowAgentInstances", TaskCatalogDbContext.DefaultSchema);

//        builder.HasKey(c => c.Id);
//        builder.HasIndex(x => x.Id).IsUnique();
//        builder.Property(x => x.Name)
//            .IsRequired();
//        builder.Property(x => x.Order)
//        .IsRequired();
//        builder.Property(x => x.Visited)
//        .IsRequired();
//        builder.Property(x => x.VisitTime);
//        builder.Property(x => x.UserId)
//       .IsRequired();
//        builder.Property(x => x.WorkflowStepInstanceId)
//      .IsRequired();
//        builder.Property(x => x.Order)
//   .IsRequired();
//        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
//    }
//}
 
