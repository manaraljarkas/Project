using ConsoleTables;
using EF_Core.Controllers;
using EF_Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Services
{
    internal class StudentMarksService
    {
        private static List<StudentMark>? _studentMarks=new();

        public StudentMarksService()
        {
            _studentMarks = StudentMarksController.GetAllStudentMarks();
        }
        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t---\tStudent Marks Table\t---\t");
                PrintTable();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Select a Student Mark To see its info");
                Console.WriteLine("2 : Add a new Student Mark");
                Console.WriteLine("3 : Update a Student Mark");
                Console.WriteLine("4 : Remove a Student Mark");
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
                        SeeFullStudentMarkInfo();
                        break;
                    case 2:
                        AddStudentMark();
                        break;
                    case 3:
                        UpdateStudentMark();
                        break;
                    case 4:
                        RemoveStudentMark();
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
            _studentMarks = StudentMarksController.GetAllStudentMarks();
            var table = new ConsoleTable
                ("ID","Exam ID", "Student ID", "Mark");
            int i = 1;
            foreach (var studentMark in _studentMarks)
            {
                table.AddRow(
                    i++,
                    studentMark.ExamId, studentMark.StudentId,
                    studentMark.Marks);
            }
            table.Write();
        }

        private static void SeeFullStudentMarkInfo()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            int index = Convert.ToInt32(Console.ReadLine());
            int? examId = _studentMarks![index - 1].ExamId;
            int? studentId = _studentMarks![index - 1].StudentId;
            var studentMark = StudentMarksController.GetStudentMark(examId, studentId);
            if (studentMark == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            Console.WriteLine("\n\n\n");

            Console.WriteLine("Student Mark Info.");
            var table = new ConsoleTable
                ("Exam ID", "Exam Subject","Student ID",
                "Student First Name", "Student Last Name",
                "Student Phone Number","Mark");
            table.AddRow(
                studentMark.ExamId, studentMark?.Exam?.Subject?.Name,studentMark?.Student?.Id,
                studentMark?.Student?.FirstName, studentMark?.Student?.LastName,
                studentMark?.Student?.Phone,studentMark?.Marks);
            table.Write();
            Console.WriteLine("\n\n\n");
            Console.ReadKey();
        }

        private static void AddStudentMark()
        {
            Console.WriteLine();
            try
            {
                StudentMark newStudentMark = new();
                Console.Write("Please Enter The Mark :  ");
                newStudentMark.Marks = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please Select a number from following Student IDs:  ");
                var students = StudentController.GetAllStudents();
                foreach (var student in students)
                {
                    Console.WriteLine(student.Id + " : " + student.FirstName + " " + student.LastName);
                }
                int s = Convert.ToInt32(Console.ReadLine());
                Student? stu = StudentController.GetStudent(s);
                newStudentMark!.StudentId = stu?.Id;

                Console.WriteLine("Please Select a number from following Exam IDs:  ");
                var exams = ExamController.GetAllExams()
                    .Where(b => b?.Subject?.DepartmentId == stu?.DepartmentId);
                foreach (var exam in exams)
                {
                    Console.WriteLine(exam.Id + " : " + exam?.Subject?.Name);
                }
                int x = Convert.ToInt32(Console.ReadLine());
                Exam ex = ExamController.GetExam(x);
                newStudentMark!.ExamId = ex?.Id;

                StudentMarksController.AddStudentMark(newStudentMark);
                Thread.Sleep(4000);
            }
            catch (Exception)
            {
                Console.WriteLine("Please Enter Valid Values To add a new Student Mark");
                Thread.Sleep(4000);
            }
        }

        private static void UpdateStudentMark()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            int index = Convert.ToInt32(Console.ReadLine());
            int? examId = _studentMarks![index-1].ExamId;
            int? studentId = _studentMarks![index-1].StudentId;
            var studentMark = StudentMarksController.GetStudentMark(examId, studentId);
            if (studentMark == null)
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
                Console.WriteLine("1 : Set a new Mark");
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
                } while (option < 0 || option > 1);
                Console.WriteLine("\n");
                switch (option)
                {
                    case 1:
                        Console.Write("Please Enter The New Mark :  ");
                        studentMark!.Marks = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 0:
                        Console.WriteLine("Do You Want To Save The New Changes? (Y/N)");
                        string? temp = Console.ReadLine();
                        if (temp != null && temp == "Y")
                            StudentMarksController.UpdateStudentMark(studentMark);
                        Thread.Sleep(4000);
                        return;
                }
            }
        }

        private static void RemoveStudentMark()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            int index = Convert.ToInt32(Console.ReadLine());
            int? examId = _studentMarks![index-1].ExamId;
            int? studentId = _studentMarks![index-1].StudentId;
            var studentMark = StudentMarksController.GetStudentMark(examId,studentId);
            if (studentMark == null)
            {
                Console.WriteLine("Next Time Enter a Valid ID");
                Thread.Sleep(3000);
                return;
            }
            StudentMarksController.RemoveStudentMark(studentMark);
            Console.WriteLine();
            Thread.Sleep(4000);
        }
    }
}
