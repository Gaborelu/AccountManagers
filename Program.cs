using AccountManagers.Data;
using AccountManagers.Models;
using AccountManagers.Utility;
using System;
using System.Configuration;

namespace AccountManagers
{
    class Program
    {
        static void Main(string[] args)
        {

            LoadApplication();
        }

        public static void LoadApplication()
        {
            Console.WriteLine("Please choose an operation:");
            Console.WriteLine("1. Display all users from database.");
            Console.WriteLine("2. Add an user to database.");
            Console.WriteLine("3. Delete an user from database.");
            Console.WriteLine("4. Quit the application");
            var option = Console.ReadLine();

            while (option.Equals("4") == false)
            {
                switch (option)
                {
                    case "1":
                        UserRepository.GetAllUsers();
                        Console.WriteLine("Please choose an operation:");
                        option = Console.ReadLine();
                        break;
                    case "2":
                        AddUser();
                        Console.WriteLine("Please choose an operation:");
                        option = Console.ReadLine();
                        break;
                    case "3":
                        RemoveUser();
                        Console.WriteLine("Please choose an operation:");
                        option = Console.ReadLine();
                        break;
                    case "4":                       
                        break;
                    default:
                        Console.WriteLine("Option was not recognized.");
                        Console.WriteLine("Please choose an operation:");
                        option = Console.ReadLine();
                        break;
                }
            }
        }

        public static void AddUser()
        {
            Console.WriteLine("Enter a name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter an email:");
            var email = Console.ReadLine();

            while (email != null)
            {
                if (EmailValidator.IsEmailValid(email) == true)
                {
                    User user = new User(name, email);
                    UserRepository.InsertUser(user);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid email.");
                    email = Console.ReadLine();
                }
                break;
            }
        }

        public static void RemoveUser()
        {
            Console.WriteLine("Enter the Id of the user:");
            var number = Console.ReadLine();
            bool success = int.TryParse(number, out int id);
            UserRepository.DeleteUser(id);
        }


    }
}
