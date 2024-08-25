using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Gadget_Gourmet.Models.Interface
{
	public interface IGeneric<T>
	{
		IEnumerable<T> GetAll();
		T GetById(int Id);
		void Insert(T Entity);
		void Update(T Entity);
		void Delete(int Id);
	}
}
