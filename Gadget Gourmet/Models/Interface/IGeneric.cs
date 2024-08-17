using Microsoft.Data.SqlClient;

namespace Gadget_Gourmet.Models.Interface
{
	public interface IGeneric<T>
	{
		// return all instances of type T
		IEnumerable<T> GetAllAsync();

		// retrieves a single instance of type T identified by ID
		T GetByIdAsync(object Id);

		// insert a new entity of type T to the database
		bool InsertAsync(T Entity);

		// update the entity of type T in the database
		bool UpdateAsync(T Entity);

		// delete the entity of type T (as generic)
		bool DeleteAsync(object Id);
	}
}
