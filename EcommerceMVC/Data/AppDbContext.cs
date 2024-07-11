using EcommerceMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace EcommerceMVC.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
	  : base(options)
		{ 
		}
		public DbSet<User> Users { get; set; }

		public DbSet<Address> Addresses { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<ProductCategory> ProductCategorys { get; set; }

		public DbSet<Cart> Carts { get; set; }

		public DbSet<OrderDetail> OrderDetails { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<WishList> WishLists { get; set; }

		public DbSet<PaymentModel> PaymentModels { get; set; }

		public DbSet<CartItem> CartItems { get; set; }

		public DbSet<Brand> Brands { get; set; }

		public DbSet<Profile> Profiles { get; set; }

        public DbSet<PrimeUser> PrimeUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(c => c.Addresses)
				.WithOne(e => e.User);

       

            modelBuilder.Entity<ProductCategory>()
				.HasMany(c => c.Products)
				.WithOne(e => e.ProductCategory);
        }
    }
}
