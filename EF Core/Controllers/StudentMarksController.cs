using EF_Core.Context;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Controllers
{
    internal class StudentMarksController
    {
        public static List<StudentMark> GetAllStudentMarks()
        {
            var _context = new ApplicationDbContext();
            var studentMarks = _context.StudentMarks.ToList();
            return studentMarks;
        }

        public static StudentMark GetStudentMark(int? eid,int? sid)
        {
            var _context = new ApplicationDbContext();
            var studentMark = _context.StudentMarks
                .Include(b=>b.Exam)
                .ThenInclude(e=> e!.Subject)
                .Include(b=>b.Student)
                .Single(b => b.ExamId == eid && b.StudentId==sid);
            return studentMark;
        }

        public static void AddStudentMark(StudentMark studentMark)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.StudentMarks.Add(studentMark);
                context.SaveChanges();
                Console.WriteLine("Student Mark Has been Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void UpdateStudentMark(StudentMark? studentMark)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Update(studentMark);
                _context.SaveChanges();
                Console.WriteLine("Student Mark Has been Updated Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void RemoveStudentMark(StudentMark studentMark)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Remove(studentMark);
                _context.SaveChanges();
                Console.WriteLine("Student Mark Has been Deleted Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
}