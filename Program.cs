using AccountManagers.Data;
using System;
using System.Configuration;

namespace AccountManagers
{
    class Program
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        static void Main(string[] args)
        {

            Console.WriteLine("Please choose an operation:");
            Console.WriteLine("1. Display all users from database.");
            Console.WriteLine("2. Add an user to database.");
            Console.WriteLine("3. Delete an user from database.");
            Console.WriteLine("4. Quit the application");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    UserRepository.DisplayUsers(connectionString);
                    break;
                case "2":
                    AddUser();
                    break;
                case "3":
                    RemoveUser();
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Option was not recognized.");
                    break;

            }
        }

        public static void AddUser()
        {
            Console.WriteLine("Enter a name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter an email:");
            var email = Console.ReadLine();
            UserRepository.InsertUser(connectionString, name, email);
        }

        public static void RemoveUser()
        {
            Console.WriteLine("Enter the Id of the user:");
            var number = Console.ReadLine();
            bool success = int.TryParse(number, out int id);
            UserRepository.DeleteUser(connectionString, id);
        }
    }
}
