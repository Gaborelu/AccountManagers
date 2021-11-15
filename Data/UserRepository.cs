using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccountManagers.Data
{
    public static class UserRepository
    {
        public static void InsertUser(string connectionString, string name, string email)
        {
            string sqlCommand = "INSERT INTO CatalinUsers(name, email) VALUES(@Name, @Email)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Connection open...");

                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);

                cmd.ExecuteNonQuery();
                Console.WriteLine("User inserted succesfuly.");

            }

        }

        public static void DeleteUser(string connectionString, int id)
        {
            string sqlCommand = "DELETE FROM CatalinUsers WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Connection open...");

                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
                Console.WriteLine("User deleted.");

            }

        }

        public static void DisplayUsers(string connectionString)
        {

            string sqlCommand = "SELECT Id,Name,Email FROM CatalinUsers";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string id = dr["Id"].ToString();
                        string name = dr["Name"].ToString();
                        string email = dr["Email"].ToString();

                        Console.WriteLine($"User with id: {id}, name: {name} and email: {email}");
                    }
                    dr.Close();
                }
            }

        }
    }
}
