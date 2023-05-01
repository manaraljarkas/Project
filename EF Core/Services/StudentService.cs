using ConsoleTables;
using EF_Core.Controllers;
using EF_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EF_Core.Services
{
    internal class StudentService
    {
        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t---\tStudent Table\t---\t");
                PrintTable();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Select a Student To see thier info");
                Console.WriteLine("2 : Add a new Student");
                Console.WriteLine("3 : Update a Student");
                Console.WriteLine("4 : Remove a Student");
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
                        SeeFullStudentInfo();
                        break;
                    case 2:
                        AddStudent();
                        break;
                    case 3:
                        UpdateStudent();
                        break;
                    case 4:
                        RemoveStudent();
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
            var students = StudentController.GetAllStudents();
            var table = new ConsoleTable
                ("ID", "Username", "First Name", "Last Name",
                "Email", "Phone Number", "Register Date", "Department");
            foreach (var student in students)
            {
                table.AddRow(student.Id, student.Username, student.FirstName, student.LastName, student.Email,
                    student.Phone, student.RegisterDate?.ToString("yyyy-MM-dd"), student?.Department?.Name);
            }
            table.Write();
        }

        private static void SeeFullStudentInfo()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var student = StudentController.GetStudent(Convert.ToInt32(Console.ReadLine()));
                if (student == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                Console.WriteLine("\n\n\n");
                Console.WriteLine("Student Info.");
                var table = new ConsoleTable
                    ("ID", "Username", "First Name", "Last Name",
                    "Email", "Phone Number", "Register Date", "Department");
                table.AddRow(
                    student.Id, student.Username, student.FirstName, student.LastName,
                    student.Email, student.Phone, student.RegisterDate?.ToString("yyyy-MM-dd"),
                    student?.Department?.Name
                    );
                table.Write();
                Console.WriteLine("\nStudent Marks Info.");
                table = new ConsoleTable
                    ("Exam ID", "Exam Date", "Exam Term", "Subject Name", "Mark");
                for (int i = 0; i < student?.StudentMarks?.Count; i++)
                {
                    StudentMark? marks = student.StudentMarks[i];
                    table.AddRow(
                    marks?.Exam?.Id, marks?.Exam?.Date, marks?.Exam?.Term, marks?.Exam?.Subject?.Name,
                    marks?.Marks);

                }
                table.Write();

                Console.WriteLine("\n\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static void AddStudent()
        {
            Console.WriteLine();
            try
            {
                Student newStudent = new Student();
                Console.Write("Please Enter The Username :  ");
                newStudent.Username = Console.ReadLine();
                Console.Write("Please Enter The First Name :  ");
                newStudent.FirstName = Console.ReadLine();
                Console.Write("Please Enter The Last Name :  ");
                newStudent.LastName = Console.ReadLine();
                Console.Write("Please Enter The Email :  ");
                newStudent.Email = Console.ReadLine();
                Console.Write("Please Enter The Phone Number :  ");
                newStudent.Phone = Console.ReadLine();
                Console.WriteLine("Please Select a number from following Departments IDs:  ");
                var departments = DepartmentController.GetAllDepartments();
                foreach (var department in departments)
                {
                    Console.WriteLine(department.Id + " : " + department.Name);
                }
                newStudent.DepartmentId = Convert.ToInt32(Console.ReadLine());
                StudentController.AddStudent(newStudent);
            }
            catch (Exception)
            {
                Console.WriteLine("Please Enter Valid Values To add a new Student");
            }
            finally { Console.ReadKey(); }
        }

        private static void UpdateStudent()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var student = StudentController.GetStudent(Convert.ToInt32(Console.ReadLine()));
                if (student == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("*Enter a Number From Following to*\n");
                    Console.WriteLine("1 : Set a new Username");
                    Console.WriteLine("2 : Set a new First Name");
                    Console.WriteLine("3 : Set a new Last Name");
                    Console.WriteLine("4 : Set a new Email");
                    Console.WriteLine("5 : Set a new Phone Number");
                    Console.WriteLine("6 : Set a new Department");
                    Console.WriteLine("0 : Exit With Save New Info (Yes/No)\n");
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
                    } while (option < 0 || option > 6);
                    Console.WriteLine("\n");
                    switch (option)
                    {
                        case 1:
                            Console.Write("Please Enter The New Username :  ");
                            student.Username = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("Please Enter The New First Name :  ");
                            student.FirstName = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("Please Enter The New Last Name :  ");
                            student.LastName = Console.ReadLine();
                            break;
                        case 4:
                            Console.Write("Please Enter The New Email :  ");
                            student.Email = Console.ReadLine();
                            break;
                        case 5:
                            Console.Write("Please Enter The New Phone Number :  ");
                            student.Phone = Console.ReadLine();
                            break;
                        case 6:
                            Console.WriteLine("Please Select a number from following Departments IDs:  ");
                            var departments = DepartmentController.GetAllDepartments();
                            foreach (var department in departments)
                            {
                                Console.WriteLine(department.Id + " : " + department.Name);
                            }

                            int x = Convert.ToInt32(Console.ReadLine());
                            student.Department = DepartmentController.GetDepartment(x);
                            break;
                        case 0:
                            Console.WriteLine("Do You Want To Save The New Changes? (Y/N)");
                            string? temp = Console.ReadLine();
                            if (temp != null && temp == "Y")
                                StudentController.UpdateStudent(student);
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static void RemoveStudent()
        {
            try
            {
                Console.WriteLine("\n*Please Select An ID From Above Table*\n");
                var student = StudentController.GetStudent(Convert.ToInt32(Console.ReadLine()));
                if (student == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                StudentController.RemoveStudent(student);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.ReadKey(); }
        }
    }
}
