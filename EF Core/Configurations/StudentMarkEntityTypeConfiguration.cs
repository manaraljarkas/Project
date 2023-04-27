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
    internal class StudentMarkEntityTypeConfiguration : IEntityTypeConfiguration<StudentMark>
    {
        public void Configure(EntityTypeBuilder<StudentMark> builder)
        {
            builder.Property(b=>b.Marks).HasMaxLength(3).IsRequired();
        }
    }
}
