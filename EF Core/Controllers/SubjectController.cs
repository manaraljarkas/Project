using EF_Core.Context;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Controllers
{
    internal class SubjectController
    {

        public static List<Subject> GetAllSubjects()
        {
            var _context = new ApplicationDbContext();
            var subjects = _context.Subjects.ToList();
            return subjects;
        }

        public static List<Subject> GetAllSubjectsByDepartmentId(int? id)
        {
            var _context = new ApplicationDbContext();
            var subjects = _context.Subjects
                .Where(s=>s.DepartmentId==id)
                .ToList();
            return subjects;
        }

        public static Subject GetSubject(int? id)
        {
            var _context = new ApplicationDbContext();
            var subject = _context.Subjects
                .Include(b => b.Department)
                .Include(b => b.Exams)
                .Include(b => b.Lectures)
                .Single(b => b.Id == id);

            return subject;
        }

        public static void AddSubject(Subject subject)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Subjects.Add(subject);
                context.SaveChanges();
                Console.WriteLine("Subject Has been Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void UpdateSubject(Subject subject)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Update(subject);
                _context.SaveChanges();
                Console.WriteLine("Subject Has been Updated Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void RemoveSubject(Subject subject)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Remove(subject);
                _context.SaveChanges();
                Console.WriteLine("Subject Has been Deleted Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
}