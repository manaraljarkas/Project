using ConsoleTables;
using EF_Core.Controllers;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EF_Core.Services
{
    internal class ReportsService
    {
        public static void RunDepartmentStudents()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Print All Department's Students");
                Console.WriteLine("2 : Print One Department Students");
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
                } while (option < 0 || option > 2);
                Console.WriteLine("\n");
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        var departments = DepartmentController.GetAllDepartments();
                        foreach (var department in departments)
                        {
                            var table = new ConsoleTable
                                ("ID", "Name");
                            table.AddRow(department.Id, department.Name);
                            table.Write();
                            var students = StudentController.GetAllStudentsByDepartmentId(department.Id);
                            table = new ConsoleTable
                             ("ID", "Username", "First Name", "Last Name",
                             "Email", "Phone Number", "Register Date");
                            foreach (var student in students)
                            {
                                table.AddRow(student.Id, student.Username, student.FirstName
                                    , student.LastName, student.Email,
                                    student.Phone, student.RegisterDate?.ToString("yyyy-MM-dd"));
                            }
                            table.Write();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Please Select a number from following Departments IDs:  ");
                        departments = DepartmentController.GetAllDepartments();
                        foreach (var department in departments)
                        {
                            Console.WriteLine(department.Id + " : " + department.Name);
                        }
                        int? DepartmentId = Convert.ToInt32(Console.ReadLine());
                        Department d = departments.First(x=>x.Id==DepartmentId);
                        var t = new ConsoleTable
                               ("ID", "Name");
                        t.AddRow(d.Id, d.Name);
                        t.Write();
                        var ss = StudentController.GetAllStudentsByDepartmentId(DepartmentId);
                        t = new ConsoleTable
                         ("ID", "Username", "First Name", "Last Name",
                         "Email", "Phone Number", "Register Date");
                        foreach (var student in ss)
                        {
                            t.AddRow(student.Id, student.Username, student.FirstName
                                , student.LastName, student.Email,
                                student.Phone, student.RegisterDate?.ToString("yyyy-MM-dd"));
                        }
                        t.Write();
                        break;
                }
                Console.ReadKey();
            }
        }

        public static void RunStudentsNotTookExams()
        {
            List<Exam> MyExams = new();
            bool tr = true;
            while (tr)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Add an Exam");
                Console.WriteLine("0 : Exit And Show Students\n");
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
                    case 0:
                        tr = false;
                        break;
                    case 1:
                        Console.WriteLine("Please Select a number from following Exam IDs:  ");
                        var exams = ExamController.GetAllExams();
                        foreach (var exam in exams)
                        {
                            if (!MyExams.Any(b=>b.Id==exam.Id))
                                Console.WriteLine(exam.Id + " : " + exam?.Subject?.Name);
                        }
                        int x = Convert.ToInt32(Console.ReadLine());
                        Exam ex = ExamController.GetExam(x);
                        MyExams.Add(ex);
                        break;
                }
            }

            if (MyExams.Count == 0)
            {
                Console.WriteLine("Enter An Exam At least");
                return;
            }
            
            foreach (var exam in MyExams)
            {
                var table = new ConsoleTable
               ("ID", "Date", "Term", "Subject Name");
                table.AddRow(
                    exam.Id, exam.Date,
                    exam.Term, exam?.Subject?.Name);
                table.Write();
                var ex = ExamController.GetExam(exam!.Id);
                var ss = StudentController.GetAllStudentsByDepartmentId(exam?.Subject?.DepartmentId);
                for (int i = 0; i < ex?.Students?.Count; i++)
                {
                    ss.RemoveAll(b => b.Id == ex.Students.ElementAt(i).Id);
                }
                table = new ConsoleTable
                 ("ID", "Username", "First Name", "Last Name",
                 "Email", "Phone Number", "Register Date");
                foreach (var student in ss)
                {
                    table.AddRow(student.Id, student.Username, student.FirstName
                        , student.LastName, student.Email,
                        student.Phone, student.RegisterDate?.ToString("yyyy-MM-dd"));
                }
                table.Write();
                Console.WriteLine("\n");
            }
            Console.ReadKey();
        }

        public static void RunStudentsTookExams()
        {

            List<Exam> MyExams = new();
            bool tr = true;
            while (tr)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Add an Exam");
                Console.WriteLine("0 : Exit And Show Students\n");
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
                    case 0:
                        tr = false;
                        break;
                    case 1:
                        Console.WriteLine("Please Select a number from following Exam IDs:  ");
                        var exams = ExamController.GetAllExams();
                        foreach (var exam in exams)
                        {
                            if (!MyExams.Any(b => b.Id == exam.Id))
                                Console.WriteLine(exam.Id + " : " + exam?.Subject?.Name);
                        }
                        int x = Convert.ToInt32(Console.ReadLine());
                        Exam ex = ExamController.GetExam(x);
                        MyExams.Add(ex);
                        break;
                }
            }

            if (MyExams.Count == 0)
            {
                Console.WriteLine("Enter An Exam At least");
                return;
            }

            foreach (var exam in MyExams)
            {
                var table = new ConsoleTable
               ("ID", "Date", "Term", "Subject Name");
                table.AddRow(
                    exam.Id, exam.Date,
                    exam.Term, exam?.Subject?.Name);
                table.Write();
                var ex = ExamController.GetExam(exam!.Id);
                var ss = ex?.StudentMarks ?? new List<StudentMark>();
                table = new ConsoleTable
                 ("ID", "Username", "First Name", "Last Name",
                 "Email", "Phone Number", "Register Date", "Mark");
                foreach (var student in ss)
                {
                    table.AddRow(student?.Student?.Id, student?.Student?.Username, student?.Student?.FirstName
                        , student?.Student?.LastName, student?.Student?.Email,
                        student?.Student?.Phone, student?.Student?.RegisterDate?.ToString("yyyy-MM-dd"),
                        student?.Marks);
                }
                table.Write();
                Console.WriteLine("\n");
            }
            Console.ReadKey();
        }

        public static void RunStudentSubject()
        {
            Console.WriteLine("Please Select a number from following Student IDs:  ");
            var students = StudentController.GetAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine(student.Id + " : " + student.FirstName + " " + student.LastName);
            }
            int s = Convert.ToInt32(Console.ReadLine());
            Student? stu = StudentController.GetStudent(s);
            if (stu == null)
                return;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Print All Department's Subjects");
                Console.WriteLine("2 : Print One Year Subjects");
                Console.WriteLine("3 : Print One Term Subjects");
                Console.WriteLine("4 : Print a Year/Term Subject");
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
                } while (option < 0 || option > 4);
                Console.WriteLine("\n");
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        var subjects = SubjectController.GetAllSubjectsByDepartmentId(stu.DepartmentId);
                        var table = new ConsoleTable
                            ("ID", "Name", "Term", "Year", "Minimum Degree");
                        foreach (var subject in subjects)
                        {
                            table.AddRow(subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree);
                        }
                        table.Write();
                        break;
                    case 2:
                        Console.WriteLine("\n*Enter the year (1 or 2)*\n");
                        Console.Write("Number :   ");
                        option = Convert.ToInt32(Console.ReadLine());
                        subjects =
                            SubjectController.GetAllSubjectsByDepartmentId(stu.DepartmentId)
                            .Where(y => y.Year == option).ToList();
                        table = new ConsoleTable
                            ("ID", "Name", "Term", "Year", "Minimum Degree");
                        foreach (var subject in subjects)
                        {
                            table.AddRow(subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree);
                        }
                        table.Write();
                        break;
                    case 3:
                        Console.WriteLine("\n*Enter the Term (1 or 2)*\n");
                        Console.Write("Number :   ");
                        option = Convert.ToInt32(Console.ReadLine());
                        subjects =
                            SubjectController.GetAllSubjectsByDepartmentId(stu.DepartmentId)
                            .Where(y => y.Term == option).ToList();
                        table = new ConsoleTable
                            ("ID", "Name", "Term", "Year", "Minimum Degree");
                        foreach (var subject in subjects)
                        {
                            table.AddRow(subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree);
                        }
                        table.Write();
                        break;
                    case 4:
                        Console.WriteLine("\n*Enter the year (1 or 2)*\n");
                        Console.Write("Number :   ");
                        int optionYear = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\n*Enter the term (1 or 2)*\n");
                        Console.Write("Number :   ");
                        int optionTerm = Convert.ToInt32(Console.ReadLine());
                        subjects =
                            SubjectController.GetAllSubjectsByDepartmentId(stu.DepartmentId)
                            .Where(y => y.Year == optionYear && y.Term == optionTerm).ToList();
                        table = new ConsoleTable
                            ("ID", "Name", "Term", "Year", "Minimum Degree");
                        foreach (var subject in subjects)
                        {
                            table.AddRow(subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree);
                        }
                        table.Write();
                        break;

                }
                Console.ReadKey();
            }
        }

        public static void RunSubjectLecture()
        {
            Console.WriteLine("Please Select a number from following Departments IDs:  ");
            var departments = DepartmentController.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine(department.Id + " : " + department.Name);
            }
            int did = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n*Enter the year (1 or 2)*\n");
            Console.Write("Number :   ");
            int optionYear = Convert.ToInt32(Console.ReadLine());
            var subjects =
                           SubjectController.GetAllSubjectsByDepartmentId(did)
                           .Where(y => y.Year == optionYear).ToList();
            var table = new ConsoleTable
                ("ID", "Name", "Term", "Year", "Minimum Degree");
            foreach (var subject in subjects)
            {
                table.AddRow(subject.Id, subject.Name, subject.Term, subject.Year, subject.MinimumDegree);
            }
            table.Write();
            Console.WriteLine("\nPlease Enter The Subject Id To See its Lectures");
            Console.Write("Number :   ");
            int sid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nSubject Lectures Info.");
            table = new ConsoleTable
                ("Lecture ID", "Title", "Content");
            Subject? s=SubjectController.GetSubject(sid);
            for (int i = 0; i < s?.Lectures?.Count; i++)
            {
                SubjectLecture? lecture = s.Lectures[i];
                table.AddRow(lecture.Id, lecture.Title, lecture.Content);
            }
            table.Write();
            Console.ReadKey();
        }
    }
}
