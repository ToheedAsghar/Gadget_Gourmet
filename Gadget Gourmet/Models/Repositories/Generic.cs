using Gadget_Gourmet.Models.Interface;
using Microsoft.Data.SqlClient;
using System.Data.Common;

// Generic contains the implementation of IGeneric Interface
namespace Gadget_Gourmet.Models.Repositories
{
	public class Generic<T> : IGeneric<T> where T : class
	{
		private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GadgetGourmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

		// utitlity for getAllASync function
		protected T MapReaderToObject(SqlDataReader reader)
		{
			var entity = Activator.CreateInstance<T>();
			foreach (var property in typeof(T).GetProperties())
			{
				property.SetValue(entity, reader[property.Name]);
			}
			return entity;
		}
		void IGeneric<T>.Insert(T Entity)
		{
			try
			{
				using (SqlConnection conn = new(_connectionString))
				{
					conn.Open();
					var tableName = typeof(T).Name;
					var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
					var columnNames = string.Join(",", properties.Select(p => p.Name));
					var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

					// Validate table name and column names to prevent SQL injection
					// Example: Implement validation or use a whitelist approach

					string query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";
					using (SqlCommand cmd = new(query, conn))
					{
						foreach (var property in properties)
						{
							var parameterName = "@" + property.Name;
							var value = property.GetValue(Entity);
							// Use appropriate SqlDbType and handle null values
							cmd.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
						}

						int retVal = cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately
				// Example: Log the exception and/or rethrow or handle as needed
				throw new InvalidOperationException("An error occurred while inserting the entity.", ex);
			}
		}
		void IGeneric<T>.Update(T Entity)
		{
			using (SqlConnection conn = new(_connectionString))
			{
				conn.Open();
				var tableName = typeof(T).Name;
				var primaryKey = "Id";

				var properties = typeof(T).GetProperties().Where(p => p.Name != primaryKey);

				var setClause = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
				var Query = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} = @{primaryKey};";

				using (SqlCommand cmd = new(Query, conn))
				{
					foreach (var property in properties)
					{
						cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(Entity));
					}
					cmd.Parameters.AddWithValue("@" + primaryKey, typeof(T).GetProperty(primaryKey).GetValue(Entity));

					int retVal = cmd.ExecuteNonQuery();
				}
			}
		}
		void IGeneric<T>.Delete(int Id)
		{
			using (SqlConnection conn = new(_connectionString))
			{
				conn.Open();
				var tableName = typeof(T).Name;
				var primaryKey = "Id";

				string Query = $"Delete from {tableName} where {primaryKey}=@id";
				using (SqlCommand cmd = new(Query, conn))
				{
					cmd.Parameters.AddWithValue("@id", Id);
					cmd.ExecuteNonQuery();
				}
			}
		}
		T IGeneric<T>.GetById(int  Id)
		{
			using (SqlConnection conn = new(_connectionString))
			{
				conn.Open();
				var tableName = typeof(T).Name;
				var primaryKey = "Id";
				string? Query = $"Select * from {tableName} where {primaryKey} = @pk ";
				using (SqlCommand cmd = new(Query, conn))
				{
					cmd.Parameters.AddWithValue("@pk", Id);
					using (var Reader = cmd.ExecuteReader())
					{
						while (Reader.Read())
						{
							return MapReaderToObject(Reader);
						}
						return null; // element not found
					}
				}
			}
		}
		IEnumerable<T> IGeneric<T>.GetAll()
		{
			using (SqlConnection conn = new(_connectionString))
			{
				conn.Open();
				var tableName = typeof(T).Name;
				string? Query = $"select * from {tableName} ";

				using (SqlCommand cmd = new(Query, conn))
				{
					var Entities = new List<T>();
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Entities.Add(MapReaderToObject(reader));
						}
						return Entities;
					}
				}
			}
		}
	}
}
