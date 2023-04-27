using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Configurations
{
    internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //builder.HasKey(t => t.Id).HasName("PK_StudentKey");
            //builder.HasKey(t => new { t.Id , t.Email });
            builder.Property(x=>x.Username).HasMaxLength(255).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(255).IsRequired();
            builder.Property(x=>x.Phone).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x=>x.RegisterDate).HasDefaultValue(DateTime.UtcNow);


            builder
                .HasOne(b => b.Department)
                .WithMany(d => d.Students)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Exams).WithMany(e => e.Students)
                .UsingEntity<StudentMark>(b =>
                {
                    //1
                    b.HasOne(b => b.Student)
                   .WithMany(b => b.StudentMarks)
                   .HasForeignKey(b => b.StudentId);
                    //2
                    b.HasOne(b => b.Exam)
                   .WithMany(b => b.StudentMarks)
                   .HasForeignKey(b => b.ExamId);
                    //Composite Key
                    b.HasKey(x => new {x.StudentId,x.ExamId});

                }
                );
        }
    }
}
