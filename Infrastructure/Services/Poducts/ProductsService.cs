using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.Products;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Services.Poducts
{
    public class ProductsService : IProducts
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public ProductsService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<Product> AddProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            return product;
        }

        public async Task<List<Product>> GetAllProductsByWhichCategoryId(int whichCategoryId)
        {
            //var EFProducts = await _db.Products
            //    .Join(_db.ProductRegisters, P => P.Id, PR => PR.ProductId, (P, PR) => new { Products = P, ProductRegisters = PR })
            //    .Join(_db.CategoryProductProperties, PR => PR.ProductRegisters.CategoryProductPropertyId, CP => CP.Id, (PR, CP) => new { ProductRegisters = PR, CategoryProductProperties = CP })
            //    .Where(CP => CP.CategoryProductProperties.WhichCategoryId == whichCategoryId).ToListAsync();

            //چون نتیجه نهایی پروداکت است  سلکت از پروداکت اینر جوین بین دو تیبل است باید آن یزنیم رو ایدی های یکسان تا به تیبل دسترسی داشته باشیم اخرشم که میشه شرط
            var query = $"SELECT P.* FROM Products P INNER JOIN ProductRegisters PR ON P.Id = PR.ProductId INNER JOIN CategoryProductProperties CP ON " +
                $"PR.CategoryProductPropertyId = CP.Id WHERE CP.WhichCategoryId = {whichCategoryId}";
            using (var connection = _dapper.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _dapper.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public async Task<Product> GetProductById(long productId)
        {
            var query = $"SELECT * FROM Products WHERE Id = {productId}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Product>(query);
            }
        }

        public async Task<List<Product>> GetAllProductsByWhichCategoryIdAndByUserCretorId(int whichCategoryId, Guid userCreatorId)
        {
            var query = $"SELECT P.* FROM Products P INNER JOIN ProductRegisters PR ON P.Id = PR.ProductId INNER JOIN CategoryProductProperties CP ON " +
                $"PR.CategoryProductPropertyId = CP.Id WHERE CP.WhichCategoryId = {whichCategoryId} AND P.UserCreatorId = '{userCreatorId}'";
            using (var connection = _dapper.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public Product UpdateProduct(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            return product;
        }

        public void DeleteProduct(Product product)
        {
            _db.Entry(product).State = EntityState.Deleted;
        }

    }
}
