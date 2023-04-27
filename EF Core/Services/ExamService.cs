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
    internal class ExamService
    {

        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t---\tExam Table\t---\t");
                PrintTable();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Select a Exam To see its info");
                Console.WriteLine("2 : Add a new Exam");
                Console.WriteLine("3 : Update a Exam");
                Console.WriteLine("4 : Remove a Exam");
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
                        SeeFullExamInfo();
                        break;
                    case 2:
                        AddExam();
                        break;
                    case 3:
                        UpdateExam();
                        break;
                    case 4:
                        RemoveExam();
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
            var exams = ExamController.GetAllExams();
            var table = new ConsoleTable
                ("ID", "Date", "Term", "Subject Name");
            foreach (var exam in exams)
            {
                table.AddRow(
                    exam.Id, exam.Date,
                    exam.Term, exam?.Subject?.Name);
            }
            table.Write();
        }

        private static void SeeFullExamInfo()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            var exam = ExamController.GetExam(Convert.ToInt32(Console.ReadLine()));
            if (exam == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            Console.WriteLine("\n\n\n");

            Console.WriteLine("Exam Info.");
            var table = new ConsoleTable
                ("ID", "Date", "Term", "Subject Name");
            table.AddRow(
                exam.Id,  exam.Date,
                exam.Term, exam?.Subject?.Name);
            table.Write();
            Console.WriteLine("Subject Info.");
            table = new ConsoleTable
                ("ID", "Name", "Term", "Year", "Minimum Degree", "Department");
            table.AddRow(
                exam?.Subject?.Id, exam?.Subject?.Name,
                exam?.Subject?.Term, exam?.Subject?.Year,
                exam?.Subject?.MinimumDegree,
                exam?.Subject?.Department?.Name);
            table.Write();
            Console.WriteLine("\nStudent Marks Info.");
            table = new ConsoleTable
                ("Student ID", "First Name", "Last Name", "Phone Number", "Mark");
            for (int i = 0; i < exam?.StudentMarks?.Count; i++)
            {
                StudentMark? marks = exam.StudentMarks[i];
                table.AddRow(
                marks?.Student?.Id, marks?.Student?.FirstName,
                marks?.Student?.LastName, marks?.Student?.Phone,
                marks?.Marks);

            }
            table.Write();
            Console.WriteLine("\n\n\n");
            Console.ReadKey();
        }

        private static void AddExam()
        {
            Console.WriteLine();
            try
            {
                Exam exam = new();
                Console.Write("Please Enter The Date (yyyy-MM-dd) :  ");
                exam.Date =Convert.ToDateTime(Console.ReadLine());
                Console.Write("Please Enter The Term :  ");
                exam.Term = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Please Select a number from following Subject IDs:  ");
                var subjects = SubjectController.GetAllSubjects();
                foreach (var subject in subjects)
                {
                    Console.WriteLine(subject.Id + " : " + subject.Name);
                }
                int x = Convert.ToInt32(Console.ReadLine());
                exam.SubjectId = x;
                ExamController.AddExam(exam);
                Thread.Sleep(4000);
            }
            catch (Exception)
            {
                Console.WriteLine("Please Enter Valid Values To add a new Exam");
                Thread.Sleep(4000);
            }
        }

        private static void UpdateExam()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            var exam = ExamController.GetExam(Convert.ToInt32(Console.ReadLine()));
            if (exam == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Set a new Date");
                Console.WriteLine("2 : Set a new Term");
                Console.WriteLine("3 : Set a new Subject");
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
                } while (option < 0 || option > 3);
                Console.WriteLine("\n");
                switch (option)
                {
                    case 1:
                        Console.Write("Please Enter The New Date (yyyy-MM-dd) :  ");
                        exam.Date = Convert.ToDateTime(Console.ReadLine());
                        break;
                    case 2:
                        Console.Write("Please Enter The New Term :  ");
                        exam.Term =Convert.ToInt16(Console.ReadLine());
                        break;
                    case 3:
                        Console.WriteLine("Please Select a number from following Subject IDs:  ");
                        var subjects = SubjectController.GetAllSubjects();
                        foreach (var subject in subjects)
                        {
                            Console.WriteLine(subject.Id + " : " + subject.Name);
                        }
                        int x = Convert.ToInt32(Console.ReadLine());
                        exam.Subject = SubjectController.GetSubject(x);
                        break;
                    case 0:
                        Console.WriteLine("Do You Want To Save The New Changes? (Y/N)");
                        string? temp = Console.ReadLine();
                        if (temp != null && temp == "Y")
                            ExamController.UpdateExam(exam);
                        Thread.Sleep(4000);
                        return;
                }
            }
        }

        private static void RemoveExam()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            var exam = ExamController.GetExam(Convert.ToInt32(Console.ReadLine()));
            if (exam == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            ExamController.RemoveExam(exam);
            Console.WriteLine();
            Thread.Sleep(4000);
        }
    }
}
