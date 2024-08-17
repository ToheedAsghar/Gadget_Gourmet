using Gadget_Gourmet.Controllers;
using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Gadget_Gourmet.Models.Repositories
{
    public class UserRepository : IUser
    {
        public string? connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GadgetGourmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public bool Login(User user)
        {
            bool retVal = false;

            string? query = @"select * from [user] where (username = @un or email= @mail) and password = @pswd;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.Add("@un", SqlDbType.VarChar, 32).Value = user.UserName?.Trim()??"None";
                    cmd.Parameters.Add("@pswd", SqlDbType.VarChar, 32).Value = user.Password?.Trim()??"None";
                    cmd.Parameters.Add("@mail", SqlDbType.VarChar, 32).Value = user.Email?.Trim()??"None";
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
				string? query = @"Insert into [user] (username, email, password) values (@un, @email, @pswd)";
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

		public bool IdExists(User user)
		{
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();
				string? query = "select * from [user] where username = @un or email = @email;";
				using (SqlCommand cmd = new(query, connection))
				{
					cmd.Parameters.AddWithValue("@un", user.UserName);
					cmd.Parameters.AddWithValue("@email", user.Email);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						return reader.Read();
					}
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
				string query = "select * from [user] where username = @un";
				using (SqlCommand cmd = new(query, connection))
				{
					cmd.Parameters.Add("@un", SqlDbType.VarChar, 32).Value = un ?? (object)DBNull.Value;
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							user = new User
							(
								name: reader["name"] != DBNull.Value ? reader["name"].ToString() : null,
								username: reader["username"] != DBNull.Value ? reader["username"].ToString() : null,
								password: reader["password"] != DBNull.Value ? reader["password"].ToString() : null,
								email: reader["email"] != DBNull.Value ? reader["email"].ToString() : null,
								address: reader["address"] != DBNull.Value ? reader["address"].ToString() : null,
								phone: reader["phone"] != DBNull.Value ? reader["phone"].ToString() : null,
								gender: reader["gender"] != DBNull.Value ? reader["gender"].ToString() : null,
								dateofbirth: reader["dateofbirth"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["dateofbirth"]) : DateOnly.MinValue
							);
						}
					}
				}
			}
			return user;
		}

		public User GetUserByEmail(string? email)
		{
			User? user = null;
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();
				string query = "select * from [user] where email=@email";
				using (SqlCommand cmd = new(query, connection))
				{
					cmd.Parameters.Add("@un", SqlDbType.VarChar, 32).Value = email ?? (object)DBNull.Value;
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							user = new User
							(
								name: reader["name"] != DBNull.Value ? reader["name"].ToString() : null,
								username: reader["username"] != DBNull.Value ? reader["username"].ToString() : null,
								password: reader["password"] != DBNull.Value ? reader["password"].ToString() : null,
								email: reader["email"] != DBNull.Value ? reader["email"].ToString() : null,
								address: reader["address"] != DBNull.Value ? reader["address"].ToString() : null,
								phone: reader["phone"] != DBNull.Value ? reader["phone"].ToString() : null,
								gender: reader["gender"] != DBNull.Value ? reader["gender"].ToString() : null,
								dateofbirth: reader["dateofbirth"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["dateofbirth"]) : DateOnly.MinValue
							);
						}
					}
				}
			}
			return user;
		}

		public User GetUserByEmailOrUsername(string? Query)
		{
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();
				string query = "select * from [user] where email=@q or username=@q";
				using (SqlCommand cmd = new(query, connection))
				{
					cmd.Parameters.Add("@q", SqlDbType.VarChar, 32).Value = Query ?? (object)DBNull.Value;
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							User user = new User
							(
								name: reader["name"] != DBNull.Value ? reader["name"].ToString() : null,
								username: reader["username"] != DBNull.Value ? reader["username"].ToString() : null,
								password: reader["password"] != DBNull.Value ? reader["password"].ToString() : null,
								email: reader["email"] != DBNull.Value ? reader["email"].ToString() : null,
								address: reader["address"] != DBNull.Value ? reader["address"].ToString() : null,
								phone: reader["phone"] != DBNull.Value ? reader["phone"].ToString() : null,
								gender: reader["gender"] != DBNull.Value ? reader["gender"].ToString() : null,
								dateofbirth: reader["dateofbirth"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["dateofbirth"]) : DateOnly.MinValue
							);
							return user;
						}
					}
				}
			}
			return new User();
		}

	}
}
