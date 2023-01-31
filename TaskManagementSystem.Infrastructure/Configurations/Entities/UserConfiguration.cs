using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Infrastructure.Models.Entities;

namespace TaskManagementSystem.Infrastructure.Configurations.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsUnicode(false).HasMaxLength(60);
            builder.Property(x => x.LastName).IsRequired().IsUnicode(false).HasMaxLength(80);
        }
    }
}
