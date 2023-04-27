using EF_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Services
{
    internal class BaseService
    {
        public static void RunCRUDs()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*Please Enter a Number From Following to Choose a Table*\n");
                Console.WriteLine("1 : Students");
                Console.WriteLine("2 : Departments");
                Console.WriteLine("3 : Subjects");
                Console.WriteLine("4 : Exams");
                Console.WriteLine("5 : Student Marks");
                Console.WriteLine("6 : Subject Lectures");
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
                } while (option < 0 || option > 6);
                Console.WriteLine("\n");
                switch (option)
                {
                    case 1:
                        StudentService.Run();
                        break;
                    case 2:
                        DepartmentService.Run();
                        break;
                    case 3:
                        SubjectService.Run();
                        break;
                    case 4:
                        ExamService.Run();
                        break;
                    case 5:
                        StudentMarksService.Run();
                        break;
                    case 6:
                        SubjectLecturesService.Run();
                        break;
                    case 0:
                        return;
                }
            }
        }

        public static void RunReports()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*Please Enter a Number From Following to Choose a Report*\n");
                Console.WriteLine("1 : Department's Students");
                Console.WriteLine("2 : Students Not Took Exams");
                Console.WriteLine("3 : Students Took Exams");
                Console.WriteLine("4 : Student's Subject");
                Console.WriteLine("5 : Subject's Lecture");
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
                        ReportsService.RunDepartmentStudents();
                        break;
                    case 2:
                        ReportsService.RunStudentsNotTookExams();
                        break;
                    case 3:
                        ReportsService.RunStudentsTookExams();
                        break;
                    case 4:
                        ReportsService.RunStudentSubject();
                        break;
                    case 5:
                        ReportsService.RunSubjectLecture();
                        break;
                    case 0:
                        return;
                }
            }
        }
    }
}
