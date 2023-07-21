using Core.Domain.Entity.Card;
using Core.Domain.Entity.Categories;
using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.DriverRegister;
using Core.Domain.Entity.Products;
using Core.IdentityEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBaseContext
{
    public class ShopDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        #region DbSet

        public DbSet<CardShopping> CardShoppings { get; set; }
        public DbSet<HistoryCardShopping> HistoryCardShoppings { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<UnImportantCategory> UnImportantCategories { get; set; }
        public DbSet<WhichCategory> WhichCategories { get; set; }
        public DbSet<CategoryProductProperty> CategoryProductProperties { get; set; }
        public DbSet<ProductRegister> ProductRegisters { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<ProductRateAndComment> ProductRateAndComments { get; set; }

        #endregion

    }
}
