using MitoCodeExam.DataAccess.Interfaces;
using MitoCodeExam.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MitoCodeExam.DataAccess.Repositories
{
   public class CategoryRepository : RepositoryContextBase<Category>, ICategoryRepository
   {
      public CategoryRepository(MitoCodeExamDbContext context) : base(context)
      {
      }

      public async Task<int> CreateAsync(Category entity)
      {
         return await Context.Insert(entity);
      }

      public async Task<(ICollection<Category> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
      {
         return await ListCollection(p => p.Name.Contains(filter), page, rows);
      }

      public async Task<Category> GetItemAsync(int id)
      {
         return await Select(id);
      }

      public async Task UpdateAsync(Category entity)
      {
         await Context.UpdateEntity(entity);
      }

      public async Task DeleteAsync(int id)
      {
         await Context.Delete(new Category
         {
            Id = id
         });
      }
   }
}
