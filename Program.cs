using AccountManagers.Data;
using AccountManagers.Models;
using AccountManagers.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using static AccountManagers.Data.Constants;

namespace AccountManagers
{
    class Program
    {
        static void Main(string[] args)
        {
            string option;
            PrintOptions();
            do
            {
                option = ChooseOption();
                HandleOption(option);

            } while (ShouldContiue(option));


        }

        private static bool ShouldContiue(string option)
        {
            return option.Equals("5") == false;
        }

        public static void PrintOptions()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1. Display all users from database.");
            Console.WriteLine("2. Add an user to database.");
            Console.WriteLine("3. Delete an user from database.");
            Console.WriteLine("4. Export information of Account Managers into a CSV file.");
            Console.WriteLine("5. Quit the application");
        }

        public static string ChooseOption()
        {
            Console.WriteLine("Please choose an option:");
            return Console.ReadLine();
        }

        public static void HandleOption(string option)
        {
            try
            {              
                {
                    switch (option)
                    {
                        case "1":
                            var users = GetUsers();
                            PrintUsers(users);
                            break;
                        case "2":
                            AddUser();
                            break;
                        case "3":
                            RemoveUser();
                            break;
                        case "4":
                            ExportUsersToCSVFile();
                            break;
                        case "5":
                            break;
                        default:
                            Console.WriteLine("Option was not recognized.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static IList<User> GetUsers()
        {
            return UserRepository.GetAllUsers();
        }

        private static void PrintUsers(IList<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"User id={user.Id} with name={user.Name} and email={user.Email}");
            }
        }

        public static void AddUser()
        {
            Console.WriteLine(AskForNameMessage);
            var name = Console.ReadLine();
            Console.WriteLine(AskForEmailMessage);
            var email = Console.ReadLine();

            IEmailValidator validator = new EmailValidator();

            while (email != null)
            {
                if (validator.IsEmailValid(email))
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
            }
        }

        public static void RemoveUser()
        {
            Console.WriteLine(AskForIdMessage);
            var number = Console.ReadLine();
            bool success = int.TryParse(number, out int id);
            UserRepository.DeleteUser(id);
            Console.WriteLine("User deleted");
        }

        public static void ExportUsersToCSVFile()
        {
            var users = UserRepository.GetAllUsers();
            UsersCSVWriter.CreateCSVFile(users);
            Console.WriteLine("Data exported.");
        }
    }
}
