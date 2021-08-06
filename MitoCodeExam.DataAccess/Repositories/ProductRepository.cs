using Microsoft.EntityFrameworkCore;
using MitoCodeExam.DataAccess.Interfaces;
using MitoCodeExam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MitoCodeExam.DataAccess.Repositories
{
   public class ProductRepository : RepositoryContextBase<Product>, IProductRepository
   {
      public ProductRepository(MitoCodeExamDbContext context) : base(context)
      {
      }

      public async Task<int> CreateAsync(Product entity)
      {
         return await Context.Insert(entity);
      }

      public async Task<(ICollection<ProductInfo> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
      {
         var tupla = await List(p => p.Name.Contains(filter), page, rows);

         var collection = tupla.Item1.ToList();

         return (collection, tupla.total);
      }

      public async Task<(ICollection<ProductInfo>, int total)> List(Expression<Func<Product, bool>> predicate, int page, int rows)
      {
         var collection = await Context.Set<Product>()
            .Where(predicate).OrderBy(x => x.Id)
            .Select(p => new ProductInfo
            {
               Id           = p.Id,
               Name         = p.Name,
               CategoryName = p.Category.Name,
               UnitPrice    = p.UnitPrice
            })
            .AsNoTracking()
            .Skip((page - 1) * rows)
            .Take(rows)
            .ToListAsync();

         var totalCount = await Context.Set<Product>()
            .Where(predicate)
            .AsNoTracking()
            .CountAsync();

         return (collection.ToList(), totalCount);
      }

      public async Task<Product> GetItemAsync(int id)
      {
         return await Select(id);
      }

      public async Task UpdateAsync(Product entity)
      {
         await Context.UpdateEntity(entity);
      }

      public async Task DeleteAsync(int id)
      {
         await Context.Delete(new Product
         {
            Id = id
         });
      }
   }
}
