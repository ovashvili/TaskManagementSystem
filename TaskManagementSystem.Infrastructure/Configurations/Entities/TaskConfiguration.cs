using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Infrastructure;
using Task = TaskManagementSystem.Infrastructure.Models.Entities.Task;

namespace TaskManagementSystem.Infrastructure.Configurations.Entities
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable(nameof(Task));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsUnicode(false).HasMaxLength(60);
            builder.Property(x => x.AssignedTo).IsRequired().IsUnicode(false).HasMaxLength(30);
            builder.Property(x => x.Description).IsUnicode(false).HasMaxLength(40);
            builder.Property(x => x.AdditionalDescription).IsUnicode(false).HasMaxLength(120);
        }
    }
}
