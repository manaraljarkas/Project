using EF_Core.Context;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Controllers
{
    internal class SubjectLectureController
    {

        public static List<SubjectLecture> GetAllSubjectLectures()
        {
            var _context = new ApplicationDbContext();
            var lectures = _context.SubjectLectures.Include(b=>b.Subject).ToList();
            return lectures;
        }

        public static SubjectLecture GetSubjectLecture(int? id)
        {
            var _context = new ApplicationDbContext();
            var lecture = _context.SubjectLectures
                .Include(b => b.Subject)
                .ThenInclude(b=> b!.Department)
                .Single(b => b.Id == id);

            return lecture;
        }

        public static void AddSubjectLecture(SubjectLecture subjectLecture)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.SubjectLectures.Add(subjectLecture);
                context.SaveChanges();
                Console.WriteLine("Subject Lecture Has been Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void UpdateSubjectLecture(SubjectLecture subjectLecture)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Update(subjectLecture);
                _context.SaveChanges();
                Console.WriteLine("Subject Lecture Has been Updated Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void RemoveSubjectLecture(SubjectLecture subjectLecture)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Remove(subjectLecture);
                _context.SaveChanges();
                Console.WriteLine("Subject Lecture Has been Deleted Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
}