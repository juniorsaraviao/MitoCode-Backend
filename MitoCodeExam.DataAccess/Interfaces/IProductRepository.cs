using MitoCodeExam.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MitoCodeExam.DataAccess.Interfaces
{
   public interface IProductRepository
   {
      Task<int> CreateAsync(Product entity);
      Task DeleteAsync(int id);
      Task<(ICollection<ProductInfo> collection, int total)> GetCollectionAsync(string filter, int page, int rows);
      Task<Product> GetItemAsync(int id);
      Task UpdateAsync(Product entity);
   }
}
