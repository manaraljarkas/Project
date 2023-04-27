using ConsoleTables;
using EF_Core.Controllers;
using EF_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Services
{
    internal class DepartmentService
    {
        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t---\tDepartment Table\t---\t");
                PrintTable();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Select a Department To see its info");
                Console.WriteLine("2 : Add a new Department");
                Console.WriteLine("3 : Update a Department");
                Console.WriteLine("4 : Remove a Department");
                Console.WriteLine("5 : Re-Print The Table");
                Console.WriteLine("0 : Exit\n");
                int option = -1;
                do
                {
                    try
                    {
                        Console.Write("Number :   ");
                        option = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please Enter a Valid Value");
                    }
                } while (option < 0 || option > 5);
                Console.WriteLine("\n");
                switch (option)
                {
                    case 1:
                        SeeFullDepartmentInfo();
                        break;
                    case 2:
                        AddDepartment();
                        break;
                    case 3:
                        UpdateDepartment();
                        break;
                    case 4:
                        RemoveDepartment();
                        break;
                    case 5:
                        break;
                    case 0:
                        return;
                }
            }
        }

        private static void PrintTable()
        {
            var departments = DepartmentController.GetAllDepartments();
            var table = new ConsoleTable
                ("ID", "Name");
            foreach (var department in departments)
            {
                table.AddRow(department.Id, department.Name);
            }
            table.Write();
        }

        private static void SeeFullDepartmentInfo()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            var department = DepartmentController.GetDepartment(Convert.ToInt32(Console.ReadLine()));
            if (department == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Department Info.");
            var table = new ConsoleTable
                ("ID", "Name");
            table.AddRow(department.Id,department.Name);
            table.Write();
            Console.WriteLine("\nDepartment Students Info.");
            table = new ConsoleTable
                ("ID", "Username", "First Name", "Last Name",
                "Email", "Phone Number", "Register Date");
            for (int i = 0; i < department?.Students?.Count; i++)
            {
                Student? student = department.Students[i];
                table.AddRow(
                    student.Id, student.Username, student.FirstName, student.LastName,
                    student.Email, student.Phone, student.RegisterDate?.ToString("yyyy-MM-dd")
                    );
            }
            table.Write();
            Console.WriteLine("\nDepartment Subjects Info.");
            table = new ConsoleTable
                ("ID", "Name", "Term", "Year", "Minimum Degree");
            for (int i = 0; i < department?.Subjects?.Count; i++)
            {
                Subject? subject = department.Subjects[i];
                table.AddRow(subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree);
            }
            table.Write();
            Console.WriteLine("\n\n\n");
            Console.ReadKey();
        }

        private static void AddDepartment()
        {
            Console.WriteLine();
            try
            {
                Department newDepartment = new();
                Console.Write("Please Enter The Name :  ");
                newDepartment.Name = Console.ReadLine();
                DepartmentController.AddDepartment(newDepartment);
                Thread.Sleep(4000);
            }
            catch (Exception)
            {
                Console.WriteLine("Please Enter Valid Values To add a new Department");
                Thread.Sleep(4000);
            }
        }

        private static void UpdateDepartment()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            var department = DepartmentController.GetDepartment(Convert.ToInt32(Console.ReadLine()));
            if (department == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("*Enter a Number From Following to*\n");
            Console.WriteLine("1 : Set a new Name");
            Console.WriteLine("0 : Exit\n");
            int option = -1;
            do
            {
                try
                {
                    Console.Write("Number :   ");
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Enter a Valid Value");
                }
            } while (option < 0 || option > 1);
            Console.WriteLine("\n");
            switch (option)
            {
                case 1:
                    Console.Write("Please Enter The New Name :  ");
                    department.Name = Console.ReadLine();
                    DepartmentController.UpdateDepartment(department);
                    Thread.Sleep(4000);
                    break;
                case 0:
                    return;
            }
        }

        private static void RemoveDepartment()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            var department = DepartmentController.GetDepartment(Convert.ToInt32(Console.ReadLine()));
            if (department == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            DepartmentController.RemoveDepartment(department);
            Console.WriteLine();
            Thread.Sleep(4000);
        }
    }
}
