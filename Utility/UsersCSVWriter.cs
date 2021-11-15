using AccountManagers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagers.Utility
{
    public static class UsersCSVWriter
    {
        public static void WriteToCSVFile(User user, string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@filePath, true))
                {
                    sw.WriteLine($"{user.Id},{user.Name},{user.Email}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong: ", ex);

            }
        }
    }
}
