using Microsoft.EntityFrameworkCore;
using MitoCodeExam.Entities;

namespace MitoCodeExam.DataAccess
{
   public class MitoCodeExamDbContext : DbContext
   {
      public MitoCodeExamDbContext(DbContextOptions<MitoCodeExamDbContext> options) : base(options) 
      {
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasPrecision(8, 2);
      }

      public DbSet<Product>  Product { get; set; }
      public DbSet<Category> Category { get; set; }
   }
}
