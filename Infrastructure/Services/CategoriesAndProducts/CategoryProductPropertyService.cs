using Core.Domain.Entity.Categories;
using Core.Domain.Entity.CategoriesAndProducts;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.CategoriesAndProducts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CategoriesAndProducts
{
    public class CategoryProductPropertyService : ICategoryProductProperty
    {
        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public CategoryProductPropertyService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<CategoryProductProperty> AddCategoryProductProperty(CategoryProductProperty categoryProductProperty)
        {
            await _db.CategoryProductProperties.AddAsync(categoryProductProperty);
            return categoryProductProperty;
        }

        public async Task<CategoryProductProperty> GetCategoryProductPropertyByWhichCategoryId(int whichCategoryId)
        {
            var query = $"SELECT [Id],[WhichCategoryId],[ProductPropertyId] FROM CategoryProductProperties WHERE " +
                $"WhichCategoryId = {whichCategoryId}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<CategoryProductProperty>(query);
            }
        }

        public CategoryProductProperty UpdateCategoryProductProperty(CategoryProductProperty categoryProductProperty)
        {
            _db.Entry(categoryProductProperty).State = EntityState.Modified;
            return categoryProductProperty;
        }

        public void DeleteCategoryProductProperty(CategoryProductProperty categoryProductProperty)
        {
            _db.Entry(categoryProductProperty).State = EntityState.Deleted;
        }
    }
}
