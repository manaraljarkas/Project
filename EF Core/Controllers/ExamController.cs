using EF_Core.Context;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Controllers
{
    internal class ExamController
    {

        public static List<Exam> GetAllExams()
        {
            var _context = new ApplicationDbContext();
            var exams = _context.Exams.Include(b=>b.Subject).ToList();
            return exams;
        }

        public static Exam GetExamStudents(int? id)
        {
            var _context = new ApplicationDbContext();
            var exam = _context.Exams
               .Include(e => e.Students)
               .Single(b=>b.Id==id);
            return exam;
        }

        public static Exam GetExam(int? id)
        {
            var _context = new ApplicationDbContext();
            var exam = _context.Exams
                .Include(e=>e.Subject)
                !.ThenInclude(b=> b!.Department)
                .Include(b=>b.StudentMarks)
                !.ThenInclude(b => b.Student)
                .Include(b=>b.Students)
                .Single(b => b.Id == id);
            return exam;
        }

        public static void AddExam(Exam exam)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Exams.Add(exam);
                context.SaveChanges();
                Console.WriteLine("Exam Has been Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void UpdateExam(Exam exam)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Update(exam);
                _context.SaveChanges();
                Console.WriteLine("Exam Has been Updated Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void RemoveExam(Exam exam)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Remove(exam);
                _context.SaveChanges();
                Console.WriteLine("Exam Has been Deleted Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
}