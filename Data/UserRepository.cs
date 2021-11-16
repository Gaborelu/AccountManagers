using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using AccountManagers.Models;
using System.Data;

namespace AccountManagers.Data
{
    public static class UserRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        public static void InsertUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UserInsert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //IN Parameters
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters["@Name"].Value = user.Name;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = user.Email;

                //OUTPUT Parameters
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                var id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                Console.WriteLine($"User with id {id} inserted succesfuly.");

            }

        }
       
        public static void DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UserDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Value = id;
             
                cmd.ExecuteNonQuery();                 

            }

        }       

        public static IList<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand("UserGetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                 

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string id = dr["Id"].ToString();
                        string name = dr["Name"].ToString();
                        string email = dr["Email"].ToString();

                        var user = new User();
                        user.Id = int.Parse(id);
                        user.Name = name;
                        user.Email = email;
                        users.Add(user);
                       
                    }
                    dr.Close();
                }
            }

            return users;
        }

        
    }
}

//----------------Store Procedure-------------

//ALTER PROCEDURE[dbo].[UserGetAll]

//AS
//BEGIN
//	-- SET NOCOUNT ON added to prevent extra result sets from
//	-- interfering with SELECT statements.
//	SET NOCOUNT ON;

//--Insert statements for procedure here

//SELECT Id, Name, Email FROM dbo.CatalinUsers
//END


//ALTER PROCEDURE[dbo].[UserInsert]

//    @Name NVARCHAR(50),
//    @Email NVARCHAR(50),

//    @Id INT OUTPUT

//AS
//BEGIN
//    -- SET NOCOUNT ON added to prevent extra result sets from
//    -- interfering with SELECT statements.

//    SET NOCOUNT ON;

//--Insert statements for procedure here

//INSERT INTO dbo.CatalinUsers(Name, Email) VALUES(@NAME, @Email)


//SELECT @Id = SCOPE_IDENTITY()
//END


//ALTER PROCEDURE[dbo].[UserDelete]

//    @Id int
//AS
//BEGIN
//    -- SET NOCOUNT ON added to prevent extra result sets from
//    -- interfering with SELECT statements.

//    SET NOCOUNT ON;

//--Insert statements for procedure here

//DELETE FROM dbo.CatalinUsers WHERE Id = @Id
//END
