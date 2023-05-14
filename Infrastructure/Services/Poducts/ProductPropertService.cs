using Core.Domain.Entity.Categories;
using Core.Domain.Entity.Products;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Products;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Poducts
{
    public class ProductPropertService : IProductProperty
    {
        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public ProductPropertService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        public async Task<ProductProperty> AddProductProperty(ProductProperty productProperty)
        {
            await _db.ProductProperties.AddAsync(productProperty);
            return productProperty;
        }

        public async Task<ProductProperty> GetProductPropertyByProductPropertyId(int productPropertyId)
        {
            var query = $"SELECT * FROM ProductProperties WHERE Id = {productPropertyId}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<ProductProperty>(query);
            }
        }

        public ProductProperty UpdateProductProperty(ProductProperty productProperty)
        {
            _db.Entry(productProperty).State = EntityState.Modified;
            return productProperty;
        }

        public void DeleteProductProperty(ProductProperty productProperty)
        {
            _db.Entry(productProperty).State = EntityState.Deleted;
        }      

        #endregion


    }
}
