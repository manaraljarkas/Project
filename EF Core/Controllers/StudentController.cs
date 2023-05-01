using EF_Core.Context;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Controllers
{
    internal class StudentController
    {

        public static List<Student> GetAllStudents()
        {
            var _context = new ApplicationDbContext();
            var students = _context.Students.Include(b=>b.Department).ToList();
            return students;
        }

        public static List<Student> GetAllStudentsByDepartmentId(int? departmentId)
        {
            var _context = new ApplicationDbContext();
            var students = _context.Students.Where(x => x.DepartmentId == departmentId).ToList();
            return students;
        }
        public static Student? GetStudent(int? id)
        {
            var _context = new ApplicationDbContext();
            var student = _context.Students!
                .Include(b => b.Department)
                .Include(b => b.StudentMarks)
                .ThenInclude(b =>b.Exam)
                .ThenInclude(d => d!.Subject)
                .SingleOrDefault(b => b.Id == id);

            return student;
        }

        public static void AddStudent(Student student)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Student Has been Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void UpdateStudent(Student student)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Update(student);
                _context.SaveChanges();
                Console.WriteLine("Student Has been Updated Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void RemoveStudent(Student student)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Remove(student);
                _context.SaveChanges();
                Console.WriteLine("Student Has been Deleted Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
}