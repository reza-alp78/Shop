using Core.Domain.Entity.Products;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Poducts
{
    public class ProductsService : IPoducts
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

        public async Task<List<Product>> GetAllProducts()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _dapper.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public async Task<Product> GetProductById(int productId)
        {
            var query = $"SELECT * FROM Products WHERE Id = {productId}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Product>(query);
            }
        }

        public async Task<List<Product>> GetProductsByUserCretorId(Guid userCreatorId)
        {
            var query = $"SELECT * FROM Products WHERE UserCreatorId = {userCreatorId}";
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
