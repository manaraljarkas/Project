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
    internal class SubjectLectureEntityTypeConfiguration : IEntityTypeConfiguration<SubjectLecture>
    {
        public void Configure(EntityTypeBuilder<SubjectLecture> builder)
        {
            builder.Property(b => b.Title).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(b => b.Content).HasColumnType("text");
            builder.HasOne(c => c.Subject).WithMany(c=>c.Lectures).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
