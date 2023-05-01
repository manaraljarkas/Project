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
    internal class SubjectLecturesService
    {
        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t---\tSubject Lectures Table\t---\t");
                PrintTable();
                Console.WriteLine("\n");
                Console.WriteLine("*Enter a Number From Following to*\n");
                Console.WriteLine("1 : Select a Subject Lecture To see its info");
                Console.WriteLine("2 : Add a new Subject Lecture");
                Console.WriteLine("3 : Update a Subject Lecture");
                Console.WriteLine("4 : Remove a Subject Lecture");
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
                        SeeFullSubjectLectureInfo();
                        break;
                    case 2:
                        AddSubjectLecture();
                        break;
                    case 3:
                        UpdateSubjectLecture();
                        break;
                    case 4:
                        RemoveSubjectLecture();
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
            var subjectLectures = SubjectLectureController.GetAllSubjectLectures();
            var table = new ConsoleTable
                ("ID", "Title", "Content", "Subject Name");
            foreach (var subjectLecture in subjectLectures)
            {
                table.AddRow(
                    subjectLecture.Id, subjectLecture.Title,
                    subjectLecture.Content, subjectLecture?.Subject?.Name);
            }
            table.Write();
        }

        private static void SeeFullSubjectLectureInfo()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var subjectLecture = SubjectLectureController.GetSubjectLecture(Convert.ToInt32(Console.ReadLine()));
                if (subjectLecture == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                Console.WriteLine("\n\n\n");

                Console.WriteLine("Subject Lecture Info.");
                var table = new ConsoleTable
                    ("ID", "Title", "Content", "Subject Name");
                table.AddRow(
                    subjectLecture.Id, subjectLecture.Title,
                    subjectLecture.Content, subjectLecture?.Subject?.Name);
                table.Write();
                Console.WriteLine("Subject Info.");
                table = new ConsoleTable
                    ("ID", "Name", "Term", "Year", "Minimum Degree", "Department");
                table.AddRow(
                    subjectLecture?.Subject?.Id, subjectLecture?.Subject?.Name,
                    subjectLecture?.Subject?.Term, subjectLecture?.Subject?.Year,
                    subjectLecture?.Subject?.MinimumDegree,
                    subjectLecture?.Subject?.Department?.Name);
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

        private static void AddSubjectLecture()
        {
            Console.WriteLine();
            try
            {
                SubjectLecture newSubjectLecture = new();
                Console.Write("Please Enter The Title :  ");
                newSubjectLecture.Title = Console.ReadLine();
                Console.Write("Please Enter The Content :  ");
                newSubjectLecture.Content = Console.ReadLine();
                Console.WriteLine("Please Select a number from following Subject IDs:  ");
                var subjects = SubjectController.GetAllSubjects();
                foreach (var subject in subjects)
                {
                    Console.WriteLine(subject.Id + " : " + subject.Name);
                }
                int x = Convert.ToInt32(Console.ReadLine());
                newSubjectLecture.SubjectId = x;
                SubjectLectureController.AddSubjectLecture(newSubjectLecture);
            }
            catch (Exception)
            {
                Console.WriteLine("Please Enter Valid Values To add a new Subject Lecture");
            }
            finally { Console.ReadKey(); }
        }

        private static void UpdateSubjectLecture()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var subjectLecture = SubjectLectureController.GetSubjectLecture(Convert.ToInt32(Console.ReadLine()));
                if (subjectLecture == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("*Enter a Number From Following to*\n");
                    Console.WriteLine("1 : Set a new Title");
                    Console.WriteLine("2 : Set a new Content");
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
                            Console.Write("Please Enter The New Title :  ");
                            subjectLecture.Title = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("Please Enter The New Content :  ");
                            subjectLecture.Content = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("Please Select a number from following Subject IDs:  ");
                            var subjects = SubjectController.GetAllSubjects();
                            foreach (var subject in subjects)
                            {
                                Console.WriteLine(subject.Id + " : " + subject.Name);
                            }
                            int x = Convert.ToInt32(Console.ReadLine());
                            subjectLecture.Subject = SubjectController.GetSubject(x);
                            break;
                        case 0:
                            Console.WriteLine("Do You Want To Save The New Changes? (Y/N)");
                            string? temp = Console.ReadLine();
                            if (temp != null && temp == "Y")
                                SubjectLectureController.UpdateSubjectLecture(subjectLecture);
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.ReadKey(); }
        }


        private static void RemoveSubjectLecture()
        {
            Console.WriteLine("\n*Please Select An ID From Above Table*\n");
            try
            {
                var lecture = SubjectLectureController.GetSubjectLecture(Convert.ToInt32(Console.ReadLine()));
                if (lecture == null)
                {
                    Console.WriteLine("Next Time Enter a Valid ID");
                    return;
                }
                SubjectLectureController.RemoveSubjectLecture(lecture);
                Console.WriteLine();
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
    }
}
