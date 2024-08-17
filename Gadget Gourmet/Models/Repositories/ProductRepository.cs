//using Gadget_Gourmet.Models.Entities;
//using Gadget_Gourmet.Models.Interface;
//using System.Net.Http.Headers;

//namespace Gadget_Gourmet.Models.Repositories
//{
//	public class ProductRepository : IGeneric<Product>
//	{
//		//private readonly IGeneric<Product> _productRepository;
//		private readonly List<Product> _products;
//		public ProductRepository()
//		{
//			//_productRepository = productRepository;
//			_products = new List<Product>();
//		}

//		public void Delete(int Id)
//		{
//			/*
//			 * This searches through _products
//			 * to find the first product where its Id matches the Id you provided.
//			 */
//			var product = _products.FirstOrDefault(p => p.Id == Id);
//			if (product != null)
//			{
//				_products.Remove(product);
//			}
//		}

//		public IEnumerable<Product> GetAll()
//		{
//			return _products;
//		}

//		public Product GetById(int Id)
//		{
//			return _products.FirstOrDefault(p => (p.Id == Id));
//		}

//		public bool Insert(Product Entity)
//		{
//			//_productRepository.Insert(Entity);
//			Insert(Entity);
//			_products.Add(Entity);
//			return true; // default
//		}
//		public bool Update(Product Entity)
//		{
//			bool retVal = false;
//			var existingProduct = _products.FirstOrDefault(p => p.Id == Entity.Id);
//			if(existingProduct != null)
//			{
//				existingProduct.Name = Entity.Name;
//				existingProduct.Description = Entity.Description;
//				existingProduct.Category = Entity.Category;
//				existingProduct.Quantity = Entity.Quantity;
//				existingProduct.Price = Entity.Price;
//				existingProduct.Color = Entity.Color;
//				existingProduct.Manufacturer = Entity.Manufacturer;
//				existingProduct.Weight = Entity.Weight;

//				retVal = true;
//			}
//			return retVal;
//		}
//	}
//}
