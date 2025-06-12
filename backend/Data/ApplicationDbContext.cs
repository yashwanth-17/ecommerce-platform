using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;

namespace EcommerceAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Product entity
            builder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.Category);
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });

            // Configure Order entity
            builder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.UserId);
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure OrderItem entity
            builder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
                entity.HasOne(e => e.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure CartItem entity
            builder.Entity<CartItem>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.ProductId }).IsUnique();
                entity.HasOne(e => e.User)
                      .WithMany(u => u.CartItems)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.CartItems)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed sample products
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop Computer",
                    Description = "High-performance laptop for work and gaming",
                    Price = 999.99m,
                    StockQuantity = 50,
                    Category = "Electronics",
                    ImageUrl = "https://example.com/laptop.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Headphones",
                    Description = "Premium noise-cancelling wireless headphones",
                    Price = 199.99m,
                    StockQuantity = 100,
                    Category = "Electronics",
                    ImageUrl = "https://example.com/headphones.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Name = "Coffee Mug",
                    Description = "Ceramic coffee mug with custom design",
                    Price = 15.99m,
                    StockQuantity = 200,
                    Category = "Home & Kitchen",
                    ImageUrl = "https://example.com/mug.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}

