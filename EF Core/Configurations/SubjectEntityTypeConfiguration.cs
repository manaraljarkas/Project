using EF_Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Configurations
{
    internal class SubjectEntityTypeConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.Property(b=>b.Name).HasMaxLength(255).IsRequired();

            builder.HasOne(b=>b.Department).WithMany(b=>b.Subjects).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
