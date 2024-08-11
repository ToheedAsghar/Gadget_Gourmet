using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Gadget_Gourmet.Models.Repositories
{
    public class IUserRepository : IUser
    {
        public string? connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GadgetGourmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public bool Login(User user)
        {
            bool retVal = false;

            string? query = @"select * from user where username = @un and password = @pswd;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.Add("@un", SqlDbType.VarChar, 32).Value = user.UserName?.Trim()??"None";
                    cmd.Parameters.Add("@pswd", SqlDbType.VarChar, 32).Value = user.Password?.Trim()??"None";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        retVal = reader.HasRows;
                    }
                }
            }
            return retVal;
        }

        public bool Signup(User user)
        {
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string? query = @"Insert into user (username, email, password) values (@un,@email,@pswd)";
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.Add("@un", SqlDbType.VarChar).Value = user.UserName?.Trim() ?? "None";
                    cmd.Parameters.Add("@pswd", SqlDbType.VarChar).Value = user.Password?.Trim() ?? "Not Set";
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email?.Trim() ?? "Not Set";
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected == 1;
                }
            }
        }

        public bool PersonalInfo(User user)
        {
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string? query = @"update user set name = @name, address = @address, phone = @phone, gender = @gender, dateofbirth = @dateofbirth where username = @username";
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = user.UserName?.Trim();
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = user.Name?.Trim() ?? "None";
                    cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = user.Address?.Trim() ?? "None";
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = user.Phone?.Trim() ?? "None";
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = user.Gender?.Trim() ?? "None";
                    cmd.Parameters.Add("@dateofbirth", SqlDbType.VarChar).Value = user.DateOfBirth;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected == 1;
                }
            }
        }

        public User GetUserByUserName(string? un)
        {
            User? user = null;
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string? query = "select * from user where username = @un";
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.Add("@un", SqlDbType.VarChar, 32).Value = un?.Trim();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            user = new User
                        (
                            reader["name"].ToString(),
                            username: reader["username"].ToString(),
                            reader["password"].ToString(),
                            reader["email"].ToString(),
                            reader["address"].ToString(),
                            reader["phone"].ToString(),
                            reader["gender"].ToString(),
                            (DateOnly)reader["dateofbirth"]
                        );
                        }
                    }
                }
            }
            return user;
        }


    }
}
