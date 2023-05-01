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
    internal class SubjectService
    {
        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t---\tSubject Table\t---\t");
                PrintTable();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Select a Subject To see its info");
                Console.WriteLine("2 : Add a new Subject");
                Console.WriteLine("3 : Update a Subject");
                Console.WriteLine("4 : Remove a Subject");
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
                        SeeFullSubjectInfo();
                        break;
                    case 2:
                        AddSubject();
                        break;
                    case 3:
                        UpdateSubject();
                        break;
                    case 4:
                        RemoveSubject();
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
            var subjects = SubjectController.GetAllSubjects();
            var table = new ConsoleTable
               ("ID", "Name", "Term", "Year", "Minimum Degree");
            foreach (var subject in subjects)
            {
                table.AddRow(subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree);
            }
            table.Write();
        }

        private static void SeeFullSubjectInfo()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var subject = SubjectController.GetSubject(Convert.ToInt32(Console.ReadLine()));
                if (subject == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                Console.WriteLine("\n\n\n");
                Console.WriteLine("Subject Info.");
                var table = new ConsoleTable
                    ("ID", "Name", "Term", "Year", "Minimum Degree", "Department");
                table.AddRow(
                    subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree,
                    subject?.Department?.Name);
                table.Write();
                Console.WriteLine("\nSubject Lectures Info.");
                table = new ConsoleTable
                    ("Lecture ID", "Title", "Content");
                for (int i = 0; i < subject?.Lectures?.Count; i++)
                {
                    SubjectLecture? lecture = subject.Lectures[i];
                    table.AddRow(lecture.Id, lecture.Title, lecture.Content);

                }
                table.Write();

                Console.WriteLine("\nSubject Exams Info.");
                table = new ConsoleTable
                    ("Exam ID", "Exam Date", "Exam Term");
                for (int i = 0; i < subject?.Exams?.Count; i++)
                {
                    Exam? exam = subject.Exams[i];
                    table.AddRow(exam.Id, exam.Date, exam.Term);
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

        private static void AddSubject()
        {
            Console.WriteLine();
            try
            {
                Subject newSubject = new();
                Console.Write("Please Enter The Name :  ");
                newSubject.Name = Console.ReadLine();
                Console.Write("Please Enter The Minimum Degree :  ");
                newSubject.MinimumDegree = Convert.ToInt32(Console.ReadLine());
                Console.Write("Please Enter The Term :  ");
                newSubject.Term = Convert.ToInt16(Console.ReadLine());
                Console.Write("Please Enter The Year :  ");
                newSubject.Year = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Please Select a number from following Departments IDs:  ");
                var departments = DepartmentController.GetAllDepartments();
                foreach (var department in departments)
                {
                    Console.WriteLine(department.Id + " : " + department.Name);
                }
                newSubject.DepartmentId = Convert.ToInt32(Console.ReadLine());
                SubjectController.AddSubject(newSubject);
            }
            catch (Exception)
            {
                Console.WriteLine("Please Enter Valid Values To add a new Subject");
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static void UpdateSubject()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var subject = SubjectController.GetSubject(Convert.ToInt32(Console.ReadLine()));
                if (subject == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("*Enter a Number From Following to*\n");
                    Console.WriteLine("1 : Set a new Name");
                    Console.WriteLine("2 : Set a new Minimum Degree");
                    Console.WriteLine("3 : Set a new Term");
                    Console.WriteLine("4 : Set a new Year");
                    Console.WriteLine("5 : Set a new Department");
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
                    } while (option < 0 || option > 5);
                    Console.WriteLine("\n");
                    switch (option)
                    {
                        case 1:
                            Console.Write("Please Enter The New Name :  ");
                            subject.Name = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("Please Enter The New Minimum Degree :  ");
                            subject.MinimumDegree = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 3:
                            Console.Write("Please Enter The New Term :  ");
                            subject.Term = Convert.ToInt16(Console.ReadLine());
                            break;
                        case 4:
                            Console.Write("Please Enter The New Year :  ");
                            subject.Year = Convert.ToInt16(Console.ReadLine());
                            break;
                        case 5:
                            Console.WriteLine("Please Select a number from following Departments IDs:  ");
                            var departments = DepartmentController.GetAllDepartments();
                            foreach (var department in departments)
                            {
                                Console.WriteLine(department.Id + " : " + department.Name);
                            }
                            int x = Convert.ToInt32(Console.ReadLine());
                            subject.Department = DepartmentController.GetDepartment(x);
                            break;
                        case 0:
                            Console.WriteLine("Do You Want To Save The New Changes? (Y/N)");
                            string? temp = Console.ReadLine();
                            if (temp != null && temp == "Y")
                                SubjectController.UpdateSubject(subject);
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

        private static void RemoveSubject()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var subject = SubjectController.GetSubject(Convert.ToInt32(Console.ReadLine()));
                if (subject == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                SubjectController.RemoveSubject(subject);
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
