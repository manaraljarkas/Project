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
    internal class ExamEntityTypeConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(x=>x.Date).HasColumnType("date");
            builder.Property(x => x.Date).HasDefaultValueSql("format(getdate(),'yyyy-MM-dd')");
            builder.HasOne(b=>b.Subject).WithMany(b=>b.Exams).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
