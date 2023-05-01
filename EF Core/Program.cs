using ConsoleTables;
using EF_Core.Context;
using EF_Core.Controllers;
using EF_Core.Models;
using EF_Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseSeed();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n---\tWelcome to TCC Management\t---\n\n");
                Console.WriteLine("*Enter The Following Number To*\n");
                Console.WriteLine("1 : CRUD Operations (For all tables)");
                Console.WriteLine("2 : Reports");
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
                        BaseService.RunCRUDs();
                        break;
                    case 2:
                        BaseService.RunReports();
                        break;
                }
            }
        }

        static void DatabaseSeed()
        {
            using var context = new ApplicationDbContext();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Departments.Any())
                return;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"/script.sql";
            
            if (!File.Exists(path))
                return;

            var sql = File.ReadAllText(path);

            string[] commands = sql.Split(new string[] { "\n" },
                StringSplitOptions.RemoveEmptyEntries);


            foreach (var command in commands)
            {
                context.Database.ExecuteSqlRaw(command);
            }
        }
    }
}