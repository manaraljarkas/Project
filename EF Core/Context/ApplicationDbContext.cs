using EF_Core.Configurations;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Context
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectModels;Initial Catalog=EFProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new StudentEntityTypeConfiguration().Configure(modelBuilder.Entity<Student>());
            new DepartmentEntityTypeConfiguration().Configure(modelBuilder.Entity<Department>());
            new SubjectEntityTypeConfiguration().Configure(modelBuilder.Entity<Subject>());
            new StudentMarkEntityTypeConfiguration().Configure(modelBuilder.Entity<StudentMark>());
            new ExamEntityTypeConfiguration().Configure(modelBuilder.Entity<Exam>());
            new SubjectLectureEntityTypeConfiguration().Configure(modelBuilder.Entity<SubjectLecture>());

            //modelBuilder.Entity<Student>()
            //    .HasMany(s => s.Exams).WithMany(e => e.Students)
            //    .UsingEntity<StudentMark>(b =>
            //    {
            //        b.HasOne(b => b.Student)
            //       .WithMany(b => b.StudentMarks)
            //       .HasForeignKey(b => b.StudentId);
            //        b.HasOne(b => b.Exam)
            //       .WithMany(b => b.StudentMarks)
            //       .HasForeignKey(b => b.ExamId);
            //        b.HasKey(b => new { b.StudentId, b.ExamId });

            //    }
            //    );
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SubjectLecture> SubjectLectures { get; set;}

        public DbSet<StudentMark> StudentMarks { get; set; }
    }
}
