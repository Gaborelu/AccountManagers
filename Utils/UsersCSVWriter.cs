using AccountManagers.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagers.Utility
{
    public static class UsersCSVWriter
    {       

        public static void CreateCSVFile(IList<User> users)
        {
            var path = @"C:\temp\data.csv";
            try
            {
                using(StreamWriter sw = new StreamWriter(path))
                {
                    //header
                    sw.WriteLine("Id,Name,Email");

                    //data
                    foreach (var user in users)
                    {
                        sw.WriteLine($"{user.Id},{user.Name},{user.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
