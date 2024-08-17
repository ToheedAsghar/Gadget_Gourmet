﻿using Gadget_Gourmet.Models.Interface;
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
				if (property.Name != "Id")
				{
					property.SetValue(entity, reader[property.Name]);
				}
			}
			return entity;
		}

		bool IGeneric<T>.InsertAsync(T Entity)
		{
			using (SqlConnection conn = new(_connectionString))
			{
				conn.Open();
				var tableName = typeof(T).Name;
				var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
				var columnNames = string.Join(",", properties.Select(p => p.Name));
				var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

				string? Query = $"insert into {tableName} ({columnNames}) values ({parameterNames})";
				using (SqlCommand cmd = new(Query, conn))
				{
					foreach (var property in properties)
					{
						cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(Entity));
					}

					int retVal = cmd.ExecuteNonQuery();
					return retVal == 1;
				}
			}
		}

		bool IGeneric<T>.UpdateAsync(T Entity)
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
					return retVal == 1;
				}
			}
		}

		bool IGeneric<T>.DeleteAsync(object Id)
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
					int retVal = cmd.ExecuteNonQuery();
					return retVal == 1;
				}
			}
		}

		T IGeneric<T>.GetByIdAsync(object Id)
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

		IEnumerable<T> IGeneric<T>.GetAllAsync()
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
