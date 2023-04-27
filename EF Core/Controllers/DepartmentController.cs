using EF_Core.Context;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Controllers
{
    internal class DepartmentController
    {

        public static List<Department> GetAllDepartments()
        {
            var _context = new ApplicationDbContext();
            var departments = _context.Departments.ToList();
            return departments;
        }

        public static Department GetDepartment(int? id)
        {
            var _context = new ApplicationDbContext();
            var department = _context.Departments
                .Include(b => b.Students)
                .Include(b => b.Subjects)
                .Single(b => b.Id == id);

            return department;
        }

        public static void AddDepartment(Department department)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Departments.Add(department);
                context.SaveChanges();
                Console.WriteLine("Department Has been Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void UpdateDepartment(Department department)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Update(department);
                _context.SaveChanges();
                Console.WriteLine("Department Has been Updated Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public static void RemoveDepartment(Department department)
        {
            try
            {
                var _context = new ApplicationDbContext();
                _context.Remove(department);
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