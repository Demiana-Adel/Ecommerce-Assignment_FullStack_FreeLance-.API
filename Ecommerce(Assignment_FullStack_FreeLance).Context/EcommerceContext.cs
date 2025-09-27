using Ecommerce_Assignment_FullStack_FreeLance_.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Context
{
    public class EcommerceContext : DbContext
    {

       
        public EcommerceContext(DbContextOptions<EcommerceContext> options)
          : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductCode)
                .IsUnique();  // Makes ProductCode unique

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();

            base.OnModelCreating(modelBuilder);


        }
    }
}
