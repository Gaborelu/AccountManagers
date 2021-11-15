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
        public static void WriteToCSVFile(string id, string name, string email, string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@filePath, true))
                {
                    sw.WriteLine($"{id},{name},{email}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong: ", ex);

            }
        }
    }
}
